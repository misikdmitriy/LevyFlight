using System;
using LevyFlight.Domain.Entities;

namespace LevyFlight.Api
{
	public sealed class MinExtremum : Extremum
	{
		internal MinExtremum(Func<double[], double> function, int variablesCount) 
			: base(function, variablesCount, AlgorithmSettings.DefaultMin)
		{
		}
	}
}
