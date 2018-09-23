using System;
using CommandLine;
using LevyFlight.Business;
using LevyFlight.Domain.Contracts;

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
            var hub = new AlgorithmFacade(arguments.ToFunctionStrategy(), arguments.VariablesCount,
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
