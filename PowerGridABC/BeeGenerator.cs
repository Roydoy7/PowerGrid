using System.Collections.Generic;

namespace PowerGridABC
{
    public class BeeGenerator
    {
        private static List<Honey> HoneyLib { get; set; }
        private static void Init()
        {
            if (HoneyLib != null)
                return;
            HoneyLib = new List<Honey>();
            HoneyLib.Add(new Honey("N", 3, 1100, 8.9, 19));
            HoneyLib.Add(new Honey("C", 7, 600, 9.5, 943));
            HoneyLib.Add(new Honey("G", 8, 350, 10.7, 599));
            HoneyLib.Add(new Honey("O", 15, 300, 29, 738));
            HoneyLib.Add(new Honey("W", 35, 20, 16.5, 26));
            HoneyLib.Add(new Honey("L", 25, 10, 38, 38));
        }

        public static Bee Generate()
        {
            Init();
            var workerBee = new Bee();
            foreach (var gene in HoneyLib)
                workerBee.Add(gene.NextRandom());
            return workerBee;
        }
    }
}
