using System.Linq;

namespace PowerGridABC
{
    public static class BeeEx
    {
        public static double GetTotalPower(this Bee bee)
        {
            return bee.Select(x => x.Power * x.Count).Sum();
        }

        public static double GetLoadRate(this Bee bee, double demandPower)
        {
            var totalPower = bee.GetTotalPower();
            if (demandPower > totalPower)
                return 0;

            return demandPower / totalPower;
        }

        public static double GetAverageCost(this Bee bee, double demandPower)
        {
            var totalPower = bee.GetTotalPower();
            if (demandPower > totalPower)
                return double.PositiveInfinity;

            var totalCost = 0.0;
            foreach (var gene in bee)
                totalCost += gene.Cost * gene.Power * gene.Count;
            return totalCost / totalPower;
        }

        public static double GetAverageCO2(this Bee bee, double demandPower)
        {
            var totalPower = bee.GetTotalPower();
            if (demandPower > totalPower)
                return double.PositiveInfinity;

            var totalCo2 = 0.0;
            foreach (var gene in bee)
                totalCo2 += gene.CO2 * gene.Power * gene.Count;
            return totalCo2 / totalPower;
        }

        public static double Fitness(this Bee bee, double demandPower)
        {
            if (bee.GetTotalPower() < demandPower)
                return 0;

            var loadRate = bee.GetLoadRate(demandPower);
            var averageCost = bee.GetAverageCost(demandPower);
            var averageCo2 = bee.GetAverageCO2(demandPower);
            var fitness = loadRate + 1 / averageCost + 1 / averageCo2;

            return fitness;
        }
    }
}
