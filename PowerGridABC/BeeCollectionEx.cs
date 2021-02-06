using System.Collections.Generic;
using System.Linq;

namespace PowerGridABC
{
    public static class BeeCollectionEx
    {
        public static Bee RouletteSelect(this IEnumerable<Bee> population)
        {
            var validPopulation = population.Where(x => x.FitnessLast > 0).ToList();
            var fitnessList = validPopulation.Select(x => x.FitnessLast);
            var sum = fitnessList.Sum();
            var probablities = fitnessList.Select(x => x / sum).ToList();
            var selected = RandomSelector.RandomSelect(probablities);
            return validPopulation.ElementAt(selected);
        }
    }
}
