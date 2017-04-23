using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Autofac;

using LevyFlight.Algorithms;
using LevyFlight.ConsoleApp.DependencyInjection;
using LevyFlight.Entities;
using LevyFlight.Facade;
using LevyFlight.Services;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json.Linq;

using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;

// ReSharper disable UnusedMember.Local

namespace LevyFlight.AutoTests
{
    public class Program
    {
        private static readonly NumericSettingsFields VariablesCount = new NumericSettingsFields(2, 30, 2, 30, true);
        private static readonly NumericSettingsFields PollinatorsCount = new NumericSettingsFields(2, 30, 2, 15, true);
        private static readonly NumericSettingsFields GroupsCount = new NumericSettingsFields(2, 30, 2, 15, true);
        private static readonly NumericSettingsFields MaxGeneration = new NumericSettingsFields(100, 100, 100, 2000, false);
        private static readonly NumericSettingsFields P = new NumericSettingsFields(0.85);
        private static readonly bool IsMin = true;

        private static readonly int RepeatNumbers = 1;

        private static FunctionFacade FunctionFacade { get; set; }

        private static string _testedFunction;

        private static IEnumerable<FieldInfo> IntSettings => typeof(Program)
            .GetFields(BindingFlags.Static | BindingFlags.NonPublic)
            .Where(p => p.FieldType.IsAssignableFrom(typeof(NumericSettingsFields)));

        private static IEnumerable<FieldInfo> DoubleSettings => typeof(Program)
            .GetFields(BindingFlags.Static | BindingFlags.NonPublic)
            .Where(p => p.FieldType.IsAssignableFrom(typeof(NumericSettingsFields)));

        private static IEnumerable<FieldInfo> Settings => IntSettings.Union(DoubleSettings);

        public static void Main(string[] args)
        {
            DependencyRegistration.Register();
            InitializeFunction();

            var changableSetting = GetChangableSetting();
            var expectedResult = 0.0;
            var loggerFactory = new LoggerFactory()
                .AddConsole()
                .AddNLog();
            var logConfig = CreateLogConfiguration();

            loggerFactory.ConfigureNLog(logConfig);

            var logger = loggerFactory.CreateLogger(_testedFunction);

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
                        var algorithm = DependencyRegistration.Container
                            .ResolveNamed<LevyFlightAlgorithm>(InjectionNames.LevyFlightAlgorithmName,
                                new NamedParameter("functionFacade", FunctionFacade));

                        var result = algorithm.PolinateAsync().Result;

                        sum += result.CountFunction(Solution.Current);
                    }

                    logger.LogInformation($"{_testedFunction} tested with parameters:{GetParameters()}");
                    logger.LogInformation($"Difference = {sum / RepeatNumbers - expectedResult}\n");

                    resultFile.WriteLine($"{changableSetting.Current};{sum / RepeatNumbers};");
                } while (changableSetting.Current < changableSetting.End);
            }
        }

        private static void InitializeFunction()
        {
            var jObject = JObject.Parse(File.ReadAllText("functionName.json"));

            _testedFunction = jObject["TestedFunction"].ToObject<string>();

            FunctionFacade = AppNameResolver.ToFunctionFacade(_testedFunction);
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
                jObject.Add(property.Name, ((NumericSettingsFields)property.GetValue(null)).Value
                    .ToString(CultureInfo.InvariantCulture));
            }

            jObject.Add("IsMin", IsMin);
            return new JObject(new JProperty("AlgorithmSettings", jObject)).ToString();
        }

        private static SettingFields<double> GetChangableSetting()
        {
            foreach (var property in Settings)
            {
                var setting = property.GetValue(null) as NumericSettingsFields;
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
                sb.AppendLine(property.Name + " = " + ((NumericSettingsFields)property.GetValue(null)).Current);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
