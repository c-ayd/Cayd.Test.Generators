namespace Cayd.Test.Generators.Utility
{
    internal static partial class Utility
    {
        internal static double Lerp(double y1, double y2, double x)
        {
            if (y2 <= y1)
                return 0.0;

            if (x < y1) return 0.0;
            else if (x > y2) return 1.0;
            else return (x - y1) / (y2 - y1);
        }
    }
}
