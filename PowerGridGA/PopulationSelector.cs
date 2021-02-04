using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerGridGA
{
    public static class PopulationSelector
    {
        public static IEnumerable<DNA> SelectByPowerDemand(this IEnumerable<DNA> population, double demandPower, double coef)
        {
            return population.Where(x => x.GetTotalPower() >= demandPower * coef);
        }

        public static DNA SelectByLoadRate(this List<DNA> population, double demandPower)
        {
            //var validPopulation = new ConcurrentBag<DNA>();
            //Parallel.ForEach(population, (x) => {
            //    if (x.GetTotalPower() >= demandPower)
            //        validPopulation.Add(x);
            //});
            var validPopulation = population.Where(x => x.GetTotalPower() >= demandPower).ToList();
            var loadRates = validPopulation.Select(x => x.GetLoadRate(demandPower));
            var sum = loadRates.Sum();
            var probablities = loadRates.Select(x => x / sum).ToList();
            var selected = RandomSelector.RandomSelect(probablities);
            return validPopulation.ElementAt(selected);
        }

        public static DNA SelectByCost(this List<DNA> population, double demandPower)
        {
            //var validPopulation = new ConcurrentBag<DNA>();
            //Parallel.ForEach(population, (x) => {
            //    if (x.GetTotalPower() >= demandPower)
            //        validPopulation.Add(x);
            //});
            var validPopulation = population.Where(x => x.GetTotalPower() >= demandPower).ToList();
            var reciprocal = validPopulation.Select(x => 1 / x.GetAverageCost(demandPower));
            var sum = reciprocal.Sum();
            var probablities = reciprocal.Select(x => x / sum).ToList();
            var selected = RandomSelector.RandomSelect(probablities);
            return validPopulation.ElementAt(selected);
        }

        public static DNA SelectByCO2(this List<DNA> population, double demandPower)
        {
            //var validPopulation = new ConcurrentBag<DNA>();
            //Parallel.ForEach(population, (x) => {
            //    if (x.GetTotalPower() >= demandPower)
            //        validPopulation.Add(x);
            //});
            var validPopulation = population.Where(x => x.GetTotalPower() >= demandPower).ToList();
            var reciprocal = validPopulation.Select(x => 1 / x.GetAverageCO2(demandPower));
            var sum = reciprocal.Sum();
            var probablities = reciprocal.Select(x => x / sum).ToList();
            var selected = RandomSelector.RandomSelect(probablities);
            return validPopulation.ElementAt(selected);
        }
    }
}
