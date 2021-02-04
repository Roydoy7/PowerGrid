using System.Collections.Generic;

namespace PowerGridGA
{
    public class DNAGenrator
    {
        private static List<Gene> DNALib { get; set; }
        private static void Init()
        {
            if (DNALib != null)
                return;
            DNALib = new List<Gene>();
            DNALib.Add(new Gene("N", 3, 1100, 8.9, 19));
            DNALib.Add(new Gene("C", 7, 600, 9.5, 943));
            DNALib.Add(new Gene("G", 8, 350, 10.7, 599));
            DNALib.Add(new Gene("O", 15, 300, 29, 738));
            DNALib.Add(new Gene("W", 35, 20, 16.5, 26));
            DNALib.Add(new Gene("L", 25, 10, 38, 38));
        }

        public static DNA Generate()
        {
            Init();
            var genes = new List<Gene>();
            foreach (var gene in DNALib)
                genes.Add(gene.NextRandom());
            return new DNA(genes);
        }
    }
}
