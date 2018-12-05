using System;
using System.Linq;
using System.Reflection;

namespace LevyFlight.Examples.FunctionStrategies
{
    internal class MulticriteriaStrategyNameAttribute : Attribute
    {
        public string Name { get; }

        public MulticriteriaStrategyNameAttribute(string name)
        {
            Name = name;
        }
    }

    public static class MulticriteriaStrategyNameExtension
    {
        public static string GetMulticriteriaStrategyName(this Type type)
        {
            var attributes = type
                .GetCustomAttributes(typeof(MulticriteriaStrategyNameAttribute))
                .ToArray();

            if (!attributes.Any())
            {
                return null;
            }

            var attribute = (MulticriteriaStrategyNameAttribute)attributes.Single();

            return attribute.Name;
        }
    }
}