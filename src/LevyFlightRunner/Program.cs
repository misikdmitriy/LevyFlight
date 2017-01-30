using System.Diagnostics;
using System.IO;
using System.Threading;

using Microsoft.Extensions.Configuration;

namespace LevyFlightRunner
{
    public class Program
    {
        private static IConfigurationRoot ConfigurationRoot { get; }

        private static string WorkingDirectory { get; }
        private static int ProcessNumber { get; }
        private static string Configuration { get; }

        static Program()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            ConfigurationRoot = builder.Build();

            WorkingDirectory = ConfigurationRoot.GetSection("Settings")["WorkingDirectory"];
            ProcessNumber = int.Parse(ConfigurationRoot.GetSection("Settings")["WorkingDirectory"]);
            Configuration = ConfigurationRoot.GetSection("Settings")["Configuration"];
        }


        public static void Main(string[] args)
        {
            Compile();

            for (var i = 0; i < ProcessNumber; i++)
            {
                StartAlgorithm();
                Thread.Sleep(1000);
            }
        }

        private static void StartAlgorithm()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    WorkingDirectory = WorkingDirectory,
                    Arguments = @"/c start dotnet run -c " + Configuration,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                }
            };
            process.Start();
        }

        private static void Compile()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = WorkingDirectory,
                    FileName = "dotnet",
                    Arguments = "build"
                }
            };
            process.Start();
            process.WaitForExit();
        }
    }
}
