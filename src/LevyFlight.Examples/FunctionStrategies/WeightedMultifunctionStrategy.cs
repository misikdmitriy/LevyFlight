using System;
using System.Linq;
using LevyFlight.Common.Check;
using LevyFlight.Common.Misc;

namespace LevyFlight.Examples.FunctionStrategies
{
    public abstract class WeightedMultifunctionStrategy
    {
        protected readonly Func<double[], double>[] Functors;
        protected readonly double[] Weights;

        protected int Count => Functors.Length;

        protected WeightedMultifunctionStrategy(params Func<double[], double>[] functors)
        {
            Functors = functors;
            Weights = new double[Count];

            var random = RandomGenerator.Random;

            for (var i = 0; i < Count; i++)
            {
	            while (Math.Abs(Weights[i]) <= 1e-5)
	            {
		            Weights[i] = random.NextDouble();
				}
			}

            var k = 1 / Weights.Sum();

            for (var i = 0; i < Count; i++)
            {
                Weights[i] *= k;
            }
        }

        protected abstract double Apply(double[] args);

        public static implicit operator Func<double[], double>
            (WeightedMultifunctionStrategy strategy) => strategy.Apply;
    }

    [MulticriteriaStrategyName("product")]
    public class WeightedProductStrategy : WeightedMultifunctionStrategy
    {
        public WeightedProductStrategy(params Func<double[], double>[] functors) 
            : base(functors)
        {
        }

        protected override double Apply(double[] args)
        {
            var mul = 1.0;
            for (var i = 0; i < Count; i++)
            {
                mul *= Math.Pow(Functors[i](args), Weights[i]);
            }
            return mul;
        }
    }

    [MulticriteriaStrategyName("amount")]
    public class WeightedAmountStrategy : WeightedMultifunctionStrategy
    {
        public WeightedAmountStrategy(params Func<double[], double>[] functors) 
            : base(functors)
        {
        }

        protected override double Apply(double[] args)
        {
            var sum = 0.0;
            for (var i = 0; i < Count; i++)
            {
                sum += Functors[i](args) * Weights[i];
            }
            return sum;
        }
    }

    [MulticriteriaStrategyName("productamount")]
    public class WeightedAmountProductStrategy : WeightedMultifunctionStrategy
    {
        private const double Betta = 0.5;

        public WeightedAmountProductStrategy(params Func<double[], double>[] functors) 
            : base(functors)
        {
        }

        protected override double Apply(double[] args)
        {
            var mul = 1.0;
            var sum = 0.0;
            for (var i = 0; i < Count; i++)
            {
                mul *= Math.Pow(Functors[i](args), Weights[i]);
                sum += Functors[i](args) * Weights[i];
            }
            return Betta * sum + (1.0 - Betta) * mul;
        }
    }
}
