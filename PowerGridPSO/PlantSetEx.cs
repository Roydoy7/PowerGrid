using System.Linq;

namespace PowerGridPSO
{
    public static class PlantSetEx
    {
        public static double GetTotalPower(this PlantSet plantSet)
        {
            return plantSet.Select(x => x.Power * x.Count).Sum();
        }

        public static double GetLoadRate(this PlantSet plantSet, double demandPower)
        {
            var totalPower = plantSet.GetTotalPower();
            if (demandPower > totalPower)
                return 0;

            return demandPower / totalPower;
        }

        public static double GetAverageCost(this PlantSet plantSet, double demandPower)
        {
            var totalPower = plantSet.GetTotalPower();
            if (demandPower > totalPower)
                return double.PositiveInfinity;

            var totalCost = 0.0;
            foreach (var gene in plantSet)
                totalCost += gene.Cost * gene.Power * gene.Count;
            return totalCost / totalPower;
        }

        public static double GetAverageCO2(this PlantSet plantSet, double demandPower)
        {
            var totalPower = plantSet.GetTotalPower();
            if (demandPower > totalPower)
                return double.PositiveInfinity;

            var totalCo2 = 0.0;
            foreach (var gene in plantSet)
                totalCo2 += gene.CO2 * gene.Power * gene.Count;
            return totalCo2 / totalPower;
        }

        public static double Fitness(this PlantSet plantSet, double demandPower)
        {
            if (plantSet.GetTotalPower() < demandPower)
                return 0;

            var loadRate = plantSet.GetLoadRate(demandPower);
            var averageCost = plantSet.GetAverageCost(demandPower);
            var averageCo2 = plantSet.GetAverageCO2(demandPower);
            var fitness = loadRate + 1 / averageCost + 1 / averageCo2;

            return fitness;
        }
    }
}
