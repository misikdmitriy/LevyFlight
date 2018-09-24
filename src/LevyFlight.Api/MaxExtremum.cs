using System;
using LevyFlight.Domain.Entities;

namespace LevyFlight.Api
{
	public class MaxExtremum : Extremum
	{
		internal MaxExtremum(Func<double[], double> function, int variablesCount) 
			: base(function, variablesCount, AlgorithmSettings.DefaultMax)
		{
		}
	}
}
