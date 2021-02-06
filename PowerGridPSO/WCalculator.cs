namespace PowerGridPSO
{
    public class WCalculator
    {
        public static double WInit { get; private set; } = 0.9;
        public static double WEnd { get; private set; } = 0.4;
        public static double CalcW(int g, int maxG)
        {
            var w = (WInit - WEnd) * (maxG - g) / maxG + WEnd;
            return w;
        }
    }
}
