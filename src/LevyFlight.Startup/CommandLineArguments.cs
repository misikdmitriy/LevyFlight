using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandLine;
using LevyFlight.Domain.Entities;
using LevyFlight.Examples.FunctionStrategies;

namespace LevyFlight.Startup
{
    public class CommandLineArguments
    {
        [Option('g', "groups", Required = false, HelpText = "Groups count", Default = AlgorithmSettings.DefaultGroupsCount)]
        public int GroupsCount { get; set; }

        [Option('m', "ismin", Required = false, HelpText = "Is minimum", Default = false)]
        public bool IsMin { get; set; }

        [Option('n', "generation", Required = false, HelpText = "Number of generations", Default = AlgorithmSettings.DefaultMaxGeneration)]
        public int MaxGeneration { get; set; }

        [Option('p', "probability", Required = false, HelpText = "Probability", Default = AlgorithmSettings.DefaultP)]
        public double P { get; set; }

        [Option('c', "pollinators", Required = false, HelpText = "Pollinators count", Default = AlgorithmSettings.DefaultPollinatorsCount)]
        public int PollinatorsCount { get; set; }

        [Option('r', "reset", Required = false, HelpText = "Reset probability", Default = AlgorithmSettings.DefaultPReset)]
        public double PReset { get; set; }

        [Option('v', "variables", Required = false, HelpText = "Variables count", Default = 30)]
        public int VariablesCount { get; set; }

        [Option('f', "functions", Required = false, HelpText = "Function name(s)", Min = 1, Max = 10, Default = new[] { "griewank" })]
        public IEnumerable<string> FunctionNames { get; set; }

        [Option('s', "strategy", Required = false, HelpText = "Multicriteria scalarization strategy", Default = "product")]
        public string MulticriteriaStrategyName { get; set; }

        internal AlgorithmSettings ToModifiedAlgorithmSettings()
        {
            return new AlgorithmSettings(GroupsCount, IsMin, MaxGeneration,
                P, PollinatorsCount, PReset);
        }

        internal Func<double[], double> ToFunctionStrategy()
        {
            var types = typeof(FunctionStrategies)
                .GetFields(BindingFlags.Static | BindingFlags.Public);

            var functions = new List<Func<double[], double>>();

            foreach (var functionName in FunctionNames)
            {
                foreach (var type in types)
                {
                    if (type.Name.Equals(functionName, StringComparison.OrdinalIgnoreCase))
                    {
                        functions.Add((Func<double[], double>)type.GetValue(null));
                        break;
                    }
                }
            }

            if (functions.Count != FunctionNames.Count())
            {
                throw new ArgumentException(nameof(FunctionNames));
            }

            if (functions.Count == 1)
            {
                return functions.Single();
            }

            return GetMultifunctionStrategy(functions.ToArray());
        }

        private WeightedMultifunctionStrategy GetMultifunctionStrategy(params Func<double[], double>[] functions)
        {
            var types = typeof(WeightedMultifunctionStrategy)
                .Assembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(WeightedMultifunctionStrategy)));

            foreach (var type in types)
            {
                if (type.GetMulticriteriaStrategyName().Equals(MulticriteriaStrategyName, StringComparison.OrdinalIgnoreCase))
                {
                    return (WeightedMultifunctionStrategy)Activator
                        .CreateInstance(type, functions);
                }
            }

            throw new ArgumentException(nameof(MulticriteriaStrategyName));
        }
    }
}
