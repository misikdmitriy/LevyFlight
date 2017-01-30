using System.Diagnostics;
using System.Threading;

namespace LevyFlightRunner
{
    public class Program
    {
        private static string WorkingDirectory => @"C:\Users\dmmisik\Downloads\Diploma\LevyFlightSharp\src\LevyFlightSharp";
        private const int ProcessNumber = 10;

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
                    Arguments = @"/c start dotnet run -c Release",
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
