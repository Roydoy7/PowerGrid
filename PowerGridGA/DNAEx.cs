using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerGridGA
{
    public static class DNAEx
    {
        public static double GetTotalPower(this DNA dna)
        {
            return dna.Select(x => x.Power * x.Count).Sum();
        }

        public static double GetLoadRate(this DNA dna, double demandPower)
        {
            var totalPower = dna.GetTotalPower();
            if (demandPower > totalPower)
                return 0;

            return demandPower / totalPower;
        }

        public static double GetAverageCost(this DNA dna, double demandPower)
        {
            var totalPower = dna.GetTotalPower();
            if (demandPower > totalPower)
                return double.PositiveInfinity;

            var totalCost = 0.0;
            foreach (var gene in dna)
                totalCost += gene.Cost * gene.Power * gene.Count;
            return totalCost / totalPower;
        }

        public static double GetAverageCO2(this DNA dna, double demandPower)
        {
            var totalPower = dna.GetTotalPower();
            if (demandPower > totalPower)
                return double.PositiveInfinity;

            var totalCo2 = 0.0;
            foreach (var gene in dna)
                totalCo2 += gene.CO2 * gene.Power * gene.Count;
            return totalCo2 / totalPower;
        }

        private static Random Rand = new Random();
        public static Tuple<DNA, DNA> CrossOver(this DNA dna1, DNA dna2)
        {
            var pos = Rand.Next(1, DNA.DNALength);
            var newGenes1 = new List<Gene>();
            newGenes1.AddRange(dna1.Take(pos));
            newGenes1.AddRange(dna2.TakeLast(DNA.DNALength - pos));

            var newGenes2 = new List<Gene>();
            newGenes2.AddRange(dna2.Take(pos));
            newGenes2.AddRange(dna1.TakeLast(DNA.DNALength - pos));

            pos = Rand.Next(1, DNA.DNALength);
            var newGenes3 = new DNA();
            newGenes3.AddRange(newGenes1.Take(pos));
            newGenes3.AddRange(newGenes2.TakeLast(DNA.DNALength - pos));

            var newGenes4 = new DNA();
            newGenes4.AddRange(newGenes2.Take(pos));
            newGenes4.AddRange(newGenes1.TakeLast(DNA.DNALength - pos));

            return new Tuple<DNA, DNA>(newGenes3, newGenes4);
        }


        public static double Fitness(this DNA dna, double demandPower)
        {
            if (dna.GetTotalPower() < demandPower)
                return 0;

            var loadRate = dna.GetLoadRate(demandPower);
            var averageCost = dna.GetAverageCost(demandPower);
            var averageCo2 = dna.GetAverageCO2(demandPower);
            var fitness = loadRate + 1 / averageCost + 1 / averageCo2;

            return fitness;
        }
    }
}
