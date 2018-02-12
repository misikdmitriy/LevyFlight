﻿using System;
using LevyFlight.Business;
using LevyFlight.Examples.FunctionStrategies;

namespace LevyFlight.Startup
{
    public class Program
    {
        public static void Main()
        {
            var algoHub = new AlgoHub();
            var result = algoHub.FindExtremeAsync(FunctionStrategies.GriewankFunction, 30).Result;

            Console.WriteLine($"Result is {result}");
        }
    }
}