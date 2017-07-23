﻿using System;
using LevyFlight.Domain.Modified.Factories;
using LevyFlight.Entities;
using LevyFlight.Extensions;

namespace LevyFlight.Business
{
    public class AlgoHub
    {
        public double FindMinimum(Func<double[], double> function, int variablesCount)
        {
            var algorithmPerformer = new AlgorithmCreator().Create(function, variablesCount);
            var resultPollinator = algorithmPerformer.Polinate();

            return resultPollinator.CountFunction(function, Solution.Current);
        }
    }
}
