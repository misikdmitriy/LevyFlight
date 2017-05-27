using LevyFlight.ConsoleApp.DependencyInjection;
using LevyFlight.Entities;
using LevyFlight.Services;
using Microsoft.Extensions.Logging;

namespace LevyFlight.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DependencyRegistration.Register();

            var algorithm = AppNameResolver.ToAlgorithm(ConfigurationService.AppSettings.UseLogger,
                ConfigurationService.AppSettings.TestedFunction);

            var timer = new TimeCounter();

            timer.Start();
            var result = algorithm.PolinateAsync().Result;
            timer.End();

            var logger = ConfigurationService
                .LoggerFactory
                .CreateLogger("Main");

            logger.LogInformation("Result Func = "
                + result.CountFunction(Solution.Current));

            logger.LogInformation("Values = " + result.ToString(Solution.Current));
        }
    }
}
