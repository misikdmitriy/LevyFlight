using System;
using System.Reflection;
using CommandLine;
using LevyFlight.Domain.Entities;
using LevyFlight.Examples.FunctionStrategies;

namespace LevyFlight.Startup
{
    public class CommandLineArguments
    {
        [Option('g', "groups", Required = false, HelpText = "Groups count", Default = 5)]
        public int GroupsCount { get; set; }

        [Option('m', "ismin", Required = false, HelpText = "Is minimum", Default = false)]
        public bool IsMin { get; set; }

        [Option('n', "generation", Required = false, HelpText = "Number of generations", Default = 30)]
        public int MaxGeneration { get; set; }

        [Option('p', "probability", Required = false, HelpText = "Probability", Default = 0.85)]
        public double P { get; set; }

        [Option('c', "pollinators", Required = false, HelpText = "Pollinators count", Default = 5)]
        public int PollinatorsCount { get; set; }

        [Option('r', "reset", Required = false, HelpText = "Reset probability", Default = 0.01)]
        public double PReset { get; set; }

        [Option('v', "variables", Required = false, HelpText = "Variables count", Default = 30)]
        public int VariablesCount { get; set; }

        [Option('f', "function", Required = false, HelpText = "Function name", Default = "Griewank")]
        public string FunctionName { get; set; }

        internal AlgorithmSettings ToModifiedAlgorithmSettings()
        {
            return new AlgorithmSettings(GroupsCount, IsMin, MaxGeneration, 
                P, PollinatorsCount, PReset);
        }

        internal Func<double[], double> ToFunctionStrategy()
        {
            var types = typeof(FunctionStrategies)
                .GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (var type in types)
            {
                if (type.Name.StartsWith(FunctionName, StringComparison.OrdinalIgnoreCase))
                {
                    return (Func<double[], double>) type.GetValue(null);
                }
            }

            throw new ArgumentException(nameof(FunctionName));
        }
    }
}
