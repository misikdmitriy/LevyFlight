using System;
using System.Collections.Generic;
using System.Linq;
using LevyFlight.Business;
using LevyFlight.Examples.FunctionStrategies;
using Shouldly;
using Xunit;

namespace LevyFlight.Tests
{
    public class AlgoTests
    {
        private const int RepeatTest = 500;
        private const double MaxDeviation = 1e-4;

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
        [InlineData(30, 30, 0.0, 1e-8)]
        public void GriewankFunction(int variablesCount, int steps, double expected, double eps)
        {
            AssertFuncion(FunctionStrategies.GriewankFunction, variablesCount, steps, expected, eps);
        }

        [Theory]
        [InlineData(30, 30, 0.0, 1e-4)]
        [InlineData(30, 40, 0.0, 1e-5)]
        [InlineData(30, 50, 0.0, 1e-7)]
        public void AckleyFunction(int variablesCount, int steps, double expected, double eps)
        {
            AssertFuncion(FunctionStrategies.AckleyFunctionStrategy, variablesCount, steps, expected, eps);
        }

        [Theory]
        [InlineData(30, 30, 0.0, 1e-5)]
        [InlineData(30, 40, 0.0, 1e-8)]
        public void RastriginFunction(int variablesCount, int steps, double expected, double eps)
        {
            AssertFuncion(FunctionStrategies.RastriginFunction, variablesCount, steps, expected, eps);
        }

        [Theory]
        // ToDo: will be good to improve
        [InlineData(30, 15, 0.0, 29)]
        public void RosenbrockFunction(int variablesCount, int steps, double expected, double eps)
        {
            AssertFuncion(FunctionStrategies.RosenbrockFunction, variablesCount, steps, expected, eps);
        }

        [Theory]
        [InlineData(30, 20, 0.0, 1e-4)]
        [InlineData(30, 30, 0.0, 1e-8)]
        public void SphereFunction(int variablesCount, int steps, double expected, double eps)
        {
            AssertFuncion(FunctionStrategies.SphereFunction, variablesCount, steps, expected, eps);
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

            (all.Average() / eps).ShouldBeGreaterThan(MaxDeviation, 
                "Assertion EPS is too big. Decrese EPS");
        }

        private ModifiedAlgorithmSettingsDto Create(int steps)
        {
            return new ModifiedAlgorithmSettingsDto(5, true, steps, 0.85, 5, 0.01);
        }
    }
}
