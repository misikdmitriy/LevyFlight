using System;
using CommandLine;
using LevyFlight.Business;

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
            var hub = new AlgoHub();
            var result = hub.FindExtremeAsync(arguments.ToFunctionStrategy(), arguments.VariablesCount, 
                arguments.ToModifiedAlgorithmSettings()).Result;

            Console.WriteLine($"Result is {result}");
        }
    }
}
