using System;
using System.Collections.Generic;
using System.Linq;
using LevyFlight.Domain.Entities;
using LevyFlight.Examples.FunctionStrategies;
using LevyFlight.TestHelper;
using Shouldly;
using Xunit;

namespace LevyFlight.Tests
{
	[Trait("Category", "Integration")]
	public class AlgoTests
	{
		private const int RepeatTest = 500;
		private const double MinDeviation = 1e-5;

		private class FailResult
		{
			public double Delta { get; }
			public int Step { get; }

			public FailResult(double delta, int step)
			{
				Delta = delta;
				Step = step;
			}
		}

		[Theory]
		[InlineData(30, 20, 0.0, 1e-5)]
		[InlineData(30, 30, 0.0, 1e-7)]
		public void GriewankFunction(int variablesCount, int steps, double expected, double eps)
		{
			AssertFuncion(FunctionStrategies.Griewank, variablesCount, steps, expected, eps);
		}

		[Theory]
		[InlineData(30, 30, 0.0, 1e-3)]
		[InlineData(30, 40, 0.0, 1e-5)]
		[InlineData(30, 50, 0.0, 1e-6)]
		[InlineData(30, 60, 0.0, 1e-7)]
		public void AckleyFunction(int variablesCount, int steps, double expected, double eps)
		{
			AssertFuncion(FunctionStrategies.Ackley, variablesCount, steps, expected, eps);
		}

		[Theory]
		[InlineData(30, 30, 0.0, 1e-3)]
		[InlineData(30, 40, 0.0, 1e-6)]
		[InlineData(30, 50, 0.0, 1e-9)]
		public void RastriginFunction(int variablesCount, int steps, double expected, double eps)
		{
			AssertFuncion(FunctionStrategies.Rastrigin, variablesCount, steps, expected, eps);
		}

		[Theory]
		// ToDo: will be good to improve
		[InlineData(30, 20, 0.0, 29)]
		public void RosenbrockFunction(int variablesCount, int steps, double expected, double eps)
		{
			AssertFuncion(FunctionStrategies.Rosenbrock, variablesCount, steps, expected, eps);
		}

		[Theory]
		[InlineData(30, 20, 0.0, 1e-4)]
		[InlineData(30, 30, 0.0, 1e-6)]
		[InlineData(30, 40, 0.0, 1e-10)]
		public void SphereFunction(int variablesCount, int steps, double expected, double eps)
		{
			AssertFuncion(FunctionStrategies.Sphere, variablesCount, steps, expected, eps);
		}

		[Theory]
		// ToDo: will be good to improve
		[InlineData(20, 0.0, 4.0)]
		public void BileFunction(int steps, double expected, double eps)
		{
			AssertFuncion(FunctionStrategies.Bile, 2, steps, expected, eps);
		}

		[Theory]
		[InlineData(30, 90, 0.0, 1e-2)]
		public void Z1Function(int variablesCount, int steps, double expected, double eps)
		{
            var strategy = new WeightedProductStrategy(FunctionStrategies.Zdt11, 
                FunctionStrategies.Zdt12);
			AssertFuncion(strategy, variablesCount, steps, expected, eps);
		}

		private void AssertFuncion(Func<double[], double> func, 
			int variablesCount, int steps, double expected, double eps)
		{
			// Assert
			var all = new List<double>();
			var fails = new List<FailResult>();

			for (var i = 0; i < RepeatTest; i++)
			{
				var performer = new AlgorithmFacade(func,
					variablesCount, Create(steps));

				// Act
				var result = performer.FindExtremeAsync().Result;

				// Assert
				var delta = Math.Abs(expected - result);
				if (delta > eps)
				{
					fails.Add(new FailResult(delta, i));
				}

				all.Add(delta);
			}

			if (fails.Count > 0)
			{
				var failedSteps = fails.Aggregate("", (result, next) => result + next.Step + ", ");
				failedSteps.Remove(failedSteps.Length - 2);

				var deltas = fails.Aggregate("", (result, next) => result + next.Delta + ", ");
				deltas.Remove(deltas.Length - 2);

				throw new Exception($"Failed on steps {failedSteps} with delta: {deltas}." +
									$"Params - {nameof(variablesCount)} = {variablesCount}; " +
									$"{nameof(steps)} = {steps}; " +
									$"{nameof(expected)} = {expected}; " +
									$"{nameof(eps)} = {eps}");
			}

			(all.Average() / eps).ShouldBeGreaterThan(MinDeviation, 
				"Assertion EPS is too big. Decrese EPS");
		}

		private AlgorithmSettings Create(int steps)
		{
			return new AlgorithmSettings(AlgorithmSettings.DefaultGroupsCount, true, steps, AlgorithmSettings.DefaultP, 
				AlgorithmSettings.DefaultPollinatorsCount, AlgorithmSettings.DefaultPReset);
		}
	}
}
