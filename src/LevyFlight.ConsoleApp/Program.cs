using LevyFlight.ConsoleApp.DependencyInjection;
using LevyFlight.Services;

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
            algorithm.PolinateAsync().Wait();
            timer.End();
        }
    }
}
