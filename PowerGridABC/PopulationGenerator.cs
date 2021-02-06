using System.Collections.Generic;
using System.Linq;

namespace PowerGridABC
{
    public class PopulationGenerator
    {
        public static IEnumerable<Bee> Generate(int populationSize)
        {
            foreach (var _ in Enumerable.Range(0, populationSize))
                yield return BeeGenerator.Generate();
        }

    }
}
