using System;
using LevyFlight.Entities;
using LevyFlight.Entities.Visitors;

namespace LevyFlight.Extensions
{
    public static class PollinatorExtensions
    {
        private static readonly ToStringPollinatorVisitor ToStringPollinatorVisitor = new ToStringPollinatorVisitor();
        private static readonly RootCheckerPollinatorVisitor RootCheckerPollinatorVisitor = new RootCheckerPollinatorVisitor();

        public static double CountFunction(this Pollinator pollinator, Func<double[], double> functionStrategy)
        {
            return pollinator.Accept(new CountFunctionPollinatorVisitor(functionStrategy));
        }

        public static bool CheckWhetherValuesCorrect(this Pollinator pollinator)
        {
            return pollinator.Accept(RootCheckerPollinatorVisitor);
        }

        public static string ToString(this Pollinator pollinator)
        {
            return pollinator.Accept(ToStringPollinatorVisitor);
        }
    }
}
