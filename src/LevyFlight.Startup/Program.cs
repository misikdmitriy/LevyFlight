using System;
using CommandLine;
using LevyFlight.Domain.Contracts;
using LevyFlight.Examples.FunctionStrategies;
using LevyFlight.TestHelper;

namespace LevyFlight.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineArguments>(args)
                .WithParsed(Main);
        }

        private static void Main(CommandLineArguments arguments)
        {
            var multicriteria = new MultifunctionStrategy(FunctionStrategies.ZDT11,
                FunctionStrategies.ZDT12);

            var hub = new AlgorithmFacade(multicriteria, arguments.VariablesCount,
                arguments.ToModifiedAlgorithmSettings());
            hub.StepFinished += HubOnStepFinished;

            var result = hub.FindExtremeAsync().Result;

            Console.WriteLine($"Result is {string.Join(",", result)}");
        }

        private static void HubOnStepFinished(object sender, StepFinishedArgs args)
        {
        }
    }
}
