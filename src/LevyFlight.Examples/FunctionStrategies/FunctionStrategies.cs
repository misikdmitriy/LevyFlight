using System;
using System.Linq;
using LevyFlight.Common.Check;

namespace LevyFlight.Examples.FunctionStrategies
{
	public static class FunctionStrategies
	{
		#region Griewank function

		/// <summary>
		/// Griewank functor with extremum f(0, 0, ..., 0) = 0
		/// </summary>
		public static Func<double[], double> Griewank = arguments =>
		{
			var sum1 = 0.0;
			var sum2 = 1.0;

			var num = 1;
			foreach (var x in arguments)
			{
				sum1 += x * x / 4000.0;
				sum2 *= Math.Cos(x / Math.Sqrt(num));
				++num;
			}

			return sum1 - sum2 + 1.0;
		};

		#endregion

		#region Ackley function

		/// <summary>
		/// Ackley functor with extremum f(0, 0, ..., 0) = 0
		/// </summary>
		public static Func<double[], double> Ackley = arguments =>
		{
			var sum1 = 0.0;
			var sum2 = 0.0;
			var n = arguments.Length;

			foreach (var argument in arguments)
			{
				sum1 += argument * argument;
				sum2 += Math.Cos(2 * Math.PI * argument);
			}

			sum1 = -20.0 * Math.Exp(-1.0 / 5.0 * Math.Sqrt(1.0 / n * sum1));
			sum2 = Math.Exp(1.0 / n * sum2);

			return sum1 - sum2 + 20 + Math.E;
		};

		#endregion

		#region Rastrigin function

		/// <summary>
		/// Rastrigin functor with extremum f(0, 0, ..., 0) = 0
		/// </summary>
		public static Func<double[], double> Rastrigin = arguments =>
		{
			var a = 10.0;
			var an = arguments.Length * a;

			var sum1 = arguments.Sum(x => x * x - a * Math.Cos(2 * Math.PI * x));

			return an + sum1;
		};

		#endregion

		#region Rosenbrock function

		/// <summary>
		/// Rosenbrock functor with extremum f(1, 1, ..., 1) = 0
		/// </summary>
		public static Func<double[], double> Rosenbrock = arguments =>
		{
			var sum = 0.0;

			for (var i = 0; i < arguments.Length - 1; i++)
			{
				sum += (1 - arguments[i]) * (1 - arguments[i]) +
					   100 * (arguments[i + 1] - arguments[i] * arguments[i]) *
							 (arguments[i + 1] - arguments[i] * arguments[i]);
			}

			return sum;
		};

		#endregion

		#region Sphere function

		/// <summary>
		/// Sphere functor with extremum f(0, 0, ..., 0) = 0
		/// </summary>
		public static Func<double[], double> Sphere = arguments =>
		{
			return arguments.Select(a => a * a).Sum();
		};

		#endregion

		#region Bile function

		/// <summary>
		/// Bile functor with extremum f(3, 0.5) = 0
		/// </summary>
		public static Func<double[], double> Bile = arguments =>
		{
			ThrowIf.NotEqual(arguments.Length, 2);

			return (1.5 - arguments[0] + arguments[0] * arguments[1]) * (1.5 - arguments[0] + arguments[0] * arguments[1])
				+ (2.25 - arguments[0] + arguments[0] * arguments[1] * arguments[1]) * (2.25 - arguments[0] + arguments[0] * arguments[1] * arguments[1])
				+ (2.625 - arguments[0] + arguments[0] * arguments[1] * arguments[1] * arguments[1]) * (2.625 - arguments[0] + arguments[0] * arguments[1] * arguments[1] * arguments[1]);
		};

        #endregion

        #region Multicriteria

        #region F1

	    public static readonly Func<double[], double> Zdt11 = arguments =>
	    {
	        return arguments[0];
	    };

	    public static readonly Func<double[], double> Zdt12 = arguments =>
	    {
	        if (arguments[0] < 0.0)
	        {
	            return double.PositiveInfinity;
	        }

	        var sum = 1.0 + 9.0 * arguments.Skip(1).Sum() / (arguments.Length - 1.0);

	        return sum * (1 - Math.Sqrt(arguments[0] / sum));
	    };

        #endregion

        #region F2

	    public static readonly Func<double[], double> Zdt21 = arguments =>
	    {
	        return arguments[0];
	    };

	    public static readonly Func<double[], double> Zdt22 = arguments =>
	    {
	        var sum = 1.0 + 9.0 * arguments.Skip(1).Sum() / (arguments.Length - 1.0);

	        return sum * (1 - arguments[0] / sum) * (1 - arguments[0] / sum);
	    };

		#endregion

		#endregion
	}
}
