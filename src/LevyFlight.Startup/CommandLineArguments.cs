using CommandLine;
using LevyFlight.Business;

namespace LevyFlight.Startup
{
    public class CommandLineArguments
    {
        [Option('g', "groups", Required = false, HelpText = "Groups count", Default = 5)]
        public int GroupsCount { get; set; }

        [Option('m', "ismin", Required = false, HelpText = "Is minimum", Default = true)]
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

        internal ModifiedAlgorithmSettingsDto ToModifiedAlgorithmSettings()
        {
            return new ModifiedAlgorithmSettingsDto(GroupsCount, IsMin, MaxGeneration, 
                P, PollinatorsCount, PReset);
        }
    }
}
