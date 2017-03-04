using System;
using System.Linq;

using LevyFlightSharp.Entities;
using LevyFlightSharp.Services;

namespace LevyFlightSharp.Extensions
{
    public static class PollinatorsGroupExtensions
    {
        public static int IndexOf(this PollinatorsGroup group, Pollinator pollinator)
        {
            return Array.IndexOf(group.ToArray(), pollinator);
        }
    }
}
