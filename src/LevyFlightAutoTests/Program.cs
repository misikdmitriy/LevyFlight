using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace LevyFlightAutoTests
{
    public class Program
    {
        private static readonly SettingFields VariablesCount = new SettingFields(2, 30, 2, 15, true);
        private static readonly SettingFields FlowersCount = new SettingFields(2, 30, 2, 15, true);
        private static readonly SettingFields GroupsCount = new SettingFields(2, 30, 2, 15, true);
        private static readonly SettingFields MaxGenerationStart = new SettingFields(100, 5000, 100, 4000, false);

        public static void Main(string[] args)
        {
            var s = CreateJsonSettings();
            s = CreateJsonSettings();
            using (var file = new StreamWriter(File.Create("appsettings.json")))
            {
                file.WriteLine(s);
            }
        }

        private static string CreateJsonSettings()
        {
            var jObject = new JObject();

            foreach (var property in typeof(Program)
                .GetFields(BindingFlags.Static | BindingFlags.NonPublic)
                .Where(p => p.FieldType == typeof(SettingFields)))
            {
                jObject.Add(property.Name, ((SettingFields)property.GetValue(null)).Next);
            }

            return new JObject(new JProperty("AlgorithmSettings", jObject)).ToString();
        }
    }
}
