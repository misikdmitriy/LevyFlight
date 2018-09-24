using System;
using System.Linq;
using Accord.Math;
using LevyFlight.Common.Misc;
using LevyFlight.Extensions;

namespace LevyFlight.Domain.FunctionStrategies
{
	internal static class FunctionStrategies
	{
		#region Distance function

		public static Func<double[], double> DistanceFunction = arguments =>
		{
			return Math.Sqrt(arguments.Sum(argument => argument * argument));
		};

		#endregion

		#region Lambda function

		public static Func<double, double> LambdaFunction = argument =>
		{
			const double t = 0.73;
			const double y = 1.0;
			const double dy = 0.3;

			return t * Math.Exp(-argument * y) + dy;
		};

		#endregion

		#region Mantegna function

		public static Func<double, double> MantegnaFunction = argument =>
		{
			var sigmaX = Gamma.Function(argument + 1) * Math.Sin(Math.PI * argument / 2);
			var divider = Gamma.Function(argument / 2) * argument * Math.Pow(2.0, (argument - 1) / 2);
			sigmaX /= divider;
			sigmaX = Math.Pow(sigmaX, 1.0 / argument);

			var x = RandomGenerator.Random.NextGaussian(0, sigmaX);
			var y = Math.Abs(RandomGenerator.Random.NextGaussian(0, 1.0));

			return x / Math.Pow(y, 1.0 / argument);
		};

		#endregion
	}
}
