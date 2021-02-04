using System.Collections.Generic;
using System.Linq;

namespace PowerGridGA
{
    public class PopulationGenerator
    {
        public static IEnumerable<DNA> Generate(int populationSize)
        {
            var list = new List<DNA>();
            foreach (var _ in Enumerable.Range(0, populationSize))
                list.Add(DNAGenrator.Generate());
            return list;
        }

    }
}
