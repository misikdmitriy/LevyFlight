using System;
using System.Linq;
using LevyFlight.Domain.Algorithms;
using LevyFlight.Domain.Modified.Factories;
using Xunit;

namespace LevyFlight.Tests
{
    public class AlgorithmPerformerTests : IDisposable
    {
        private AlgorithmPerformer _performer;

        public AlgorithmPerformerTests()
        {
            Func<double[], double> func = arguments =>
            {
                return arguments.Sum(a => a * a);
            };

            _performer = new AlgorithmCreator().Create(func, 15);
        }

        [Fact]
        public void 

        public void Dispose()
        {
        }
    }
}
