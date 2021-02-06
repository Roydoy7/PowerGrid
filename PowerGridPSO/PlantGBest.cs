using System.Collections.Generic;

namespace PowerGridPSO
{
    public class PlantGBest
    {
        private static Dictionary<string, int> GBestDict { get; set; }
        private static void Init()
        {
            if (GBestDict != null)
                return;
            GBestDict = new Dictionary<string, int>();
            GBestDict.Add("N", 0);
            GBestDict.Add("C", 0);
            GBestDict.Add("G", 0);
            GBestDict.Add("O", 0);
            GBestDict.Add("W", 0);
            GBestDict.Add("L", 0);
        }

        public static int GetGBest(string code)
        {
            Init();
            return GBestDict[code];
        }
        public static void SetGBest(string code, int gbest)
        {
            Init();
            GBestDict[code] = gbest;
        }
    }
}
