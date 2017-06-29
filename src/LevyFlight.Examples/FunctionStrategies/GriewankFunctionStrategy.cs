﻿using LevyFlight.Strategies;
using System;

namespace LevyFlight.Examples.FunctionStrategies
{
    public class GriewankFunctionStrategy : IFunctionStrategy
    {
        public double Apply(double[] arguments)
        {
            var sum1 = 0.0;
            var sum2 = 1.0;

            var num = 1;
            foreach (var x in arguments)
            {
                sum1 += x * x / 4000.0;
                sum2 *= Math.Cos(x / Math.Sqrt(num));
                ++num;
            }

            return sum1 - sum2 + 1.0;
        }
    }
}