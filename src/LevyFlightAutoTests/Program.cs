using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using LevyFlightSharp.Algorithms;
using LevyFlightSharp.Domain;
using LevyFlightSharp.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;

namespace LevyFlightAutoTests
{
    public class Program
    {
        private static readonly IntSettingsFields VariablesCount = new IntSettingsFields(2, 30, 2, 30, true);

        private static readonly IntSettingsFields FlowersCount = new IntSettingsFields(2, 30, 2, 5, true);
        private static readonly IntSettingsFields GroupsCount = new IntSettingsFields(2, 30, 2, 5, true);

        private static readonly IntSettingsFields MaxGeneration = new IntSettingsFields(100, 4000, 100, 2000, false);

        private static readonly DoubleSettingsFields P = new DoubleSettingsFields(0.85);

        private static readonly int RepeatNumbers = 5;

        private static IEnumerable<FieldInfo> IntSettings => typeof(Program)
            .GetFields(BindingFlags.Static | BindingFlags.NonPublic)
            .Where(p => p.FieldType.IsAssignableFrom(typeof(IntSettingsFields)));

        private static IEnumerable<FieldInfo> DoubleSettings => typeof(Program)
            .GetFields(BindingFlags.Static | BindingFlags.NonPublic)
            .Where(p => p.FieldType.IsAssignableFrom(typeof(DoubleSettingsFields)));

        private static IEnumerable<FieldInfo> Settings => IntSettings.Union(DoubleSettings);

        public static void Main(string[] args)
        {
            var changableSetting = GetChangableSetting();
            var testedFunctionName = nameof(RastriginFunction);
            var expectedResult = 0.0;
            var loggerFactory = new LoggerFactory()
                .AddConsole()
                .AddNLog();
            var logConfig = CreateLogConfiguration();

            loggerFactory.ConfigureNLog(logConfig);

            var logger = loggerFactory.CreateLogger(testedFunctionName);

            using (var resultFile = new StreamWriter(File.Create("result.csv")))
            {
                do
                {
                    var s = CreateJsonSettings();
                    using (var file = new StreamWriter(
                        File.Create(
                            Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))))
                    {
                        file.WriteLine(s);
                    }

                    ConfigurationService.ReconfigureApp();

                    var sum = 0.0;
                    for (var i = 0; i < RepeatNumbers; i++)
                    {
                        var algorithm = new LevyFlightAlgorithm(RastriginFunction);
                        var result = algorithm.Polinate();

                        sum += result.CountFunction(Solution.Current);
                    }

                    logger.LogInformation($"{testedFunctionName} tested with parameters:{GetParameters()}");
                    logger.LogInformation($"Difference = {sum / RepeatNumbers - expectedResult}\n");

                    resultFile.WriteLine($"{changableSetting.Current};{sum / RepeatNumbers};");
                } while (changableSetting.Current < changableSetting.End);
            }
        }

        private static LoggingConfiguration CreateLogConfiguration()
        {
            var logConfig = new LoggingConfiguration();

            // config target
            var fileTarget = new FileTarget
            {
                Name = "logFile",
                FileName = "${basedir}/logs/${shortdate}.log",
                DeleteOldFileOnStartup = true
            };

            // config rule

            var rule = new LoggingRule("*", NLog.LogLevel.Info, fileTarget);

            logConfig.AddTarget(fileTarget);
            logConfig.LoggingRules.Add(rule);
            return logConfig;
        }

        private static string CreateJsonSettings()
        {
            var jObject = new JObject();

            foreach (var property in Settings)
            {
                jObject.Add(property.Name,
                    property.FieldType == typeof(IntSettingsFields)
                        ? ((IntSettingsFields)property.GetValue(null)).Value.ToString()
                        : ((DoubleSettingsFields)property.GetValue(null)).Value.ToString("F2", CultureInfo.InvariantCulture));
            }

            jObject.Add("IsMin", true);
            return new JObject(new JProperty("AlgorithmSettings", jObject)).ToString();
        }

        private static SettingFields<int> GetChangableSetting()
        {
            foreach (var property in Settings)
            {
                var setting = property.GetValue(null) as IntSettingsFields;
                if (setting != null && !setting.IsFixed)
                {
                    return setting;
                }
            }

            throw new InvalidOperationException("No unfixed value");
        }

        private static string GetParameters()
        {
            var sb = new StringBuilder();
            sb.AppendLine();

            foreach (var property in Settings)
            {
                if (property.FieldType == typeof(DoubleSettingsFields))
                {
                    sb.AppendLine(property.Name + " = " + ((DoubleSettingsFields)property.GetValue(null)).Current);
                }
                else
                {
                    sb.AppendLine(property.Name + " = " + ((IntSettingsFields)property.GetValue(null)).Current);
                }
            }

            return sb.ToString().TrimEnd();
        }

        private static double GriewankFunction(double[] flowers)
        {
            var sum1 = 0.0;
            var sum2 = 1.0;

            var num = 1;
            foreach (var x in flowers)
            {
                sum1 += x * x / 4000.0;
                sum2 *= Math.Cos(x / Math.Sqrt(num));
                ++num;
            }

            return sum1 - sum2 + 1.0;
        }

        private static double RastriginFunction(double[] flowers)
        {
            var a = 10.0;
            var an = flowers.Length * a;

            var sum1 = flowers.Sum(x => x * x - a * Math.Cos(2 * Math.PI * x));

            return an + sum1;
        }
    }
}
