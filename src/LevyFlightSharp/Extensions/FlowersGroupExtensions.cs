using System;
using System.Linq;

using LevyFlightSharp.Entities;
using LevyFlightSharp.Services;

namespace LevyFlightSharp.Extensions
{
    public static class FlowersGroupExtensions
    {
        public static int IndexOf(this FlowersGroup group, Flower flower)
        {
            return Array.IndexOf(group.ToArray(), flower);
        }
    }
}
