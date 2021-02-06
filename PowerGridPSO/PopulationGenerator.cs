using PowerGridPSO;
using System.Collections.Generic;
using System.Linq;

namespace PowerGridPSO
{
    public class PopulationGenerator
    {
        public static IEnumerable<PlantSet> Generate(int populationSize)
        {
            foreach (var _ in Enumerable.Range(0, populationSize))
               yield return PlantSetGenerator.Generate();
        }

    }
}
