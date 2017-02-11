namespace LevyFlightSharp.Entities
{
    public class FlowerSettings
    {
        public double Lambda { get; }
        public double P { get; }

        public FlowerSettings(double lambda, double p)
        {
            Lambda = lambda;
            P = p;
        }
    }
}
