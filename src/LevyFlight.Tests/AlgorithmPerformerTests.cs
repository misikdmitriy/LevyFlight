using System;
using System.Collections.Generic;
using LevyFlight.Domain.Modified.Algorithms;
using LevyFlight.Domain.Modified.Entities;
using LevyFlight.Entities;
using LevyFlight.Examples.FunctionStrategies;

namespace LevyFlight.Tests
{
    public class AlgorithmPerformerTests : IDisposable
    {
        private AlgorithmPerformer _performer;

        public AlgorithmPerformerTests()
        {
            var settings = ModifiedAlgorithmSettings.Default;
            var groups = new List<PollinatorsGroup>();
            var variablesCount = 15;

            for (int i = 0; i < 10; i++)
            {
                groups.Add(new PollinatorsGroup(settings.PollinatorsCount, variablesCount));
            }


            // TODO: Finish tests
            _performer = new AlgorithmPerformer(settings, groups.ToArray(), new GriewankFunctionStrategy(), null, null);
        }

        public void Dispose()
        {
        }
    }
}
