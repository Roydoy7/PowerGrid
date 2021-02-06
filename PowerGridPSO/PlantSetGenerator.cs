using System.Collections.Generic;

namespace PowerGridPSO
{
    public class PlantSetGenerator
    {
        private static List<Plant> PlantLib { get; set; }
        private static void Init()
        {
            if (PlantLib != null)
                return;
            PlantLib = new List<Plant>();
            PlantLib.Add(new Plant("N", 3, 1100, 8.9, 19));
            PlantLib.Add(new Plant("C", 7, 600, 9.5, 943));
            PlantLib.Add(new Plant("G", 8, 350, 10.7, 599));
            PlantLib.Add(new Plant("O", 15, 300, 29, 738));
            PlantLib.Add(new Plant("W", 35, 20, 16.5, 26));
            PlantLib.Add(new Plant("L", 25, 10, 38, 38));
        }

        public static PlantSet Generate()
        {
            Init();
            var plantSet = new PlantSet();
            foreach (var gene in PlantLib)
                plantSet.Add(gene.NextRandom());
            return plantSet;
        }
    }
}
