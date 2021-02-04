using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerGridGA
{
    class Program
    {
        private static Random Rand = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var maxGeneration = 2000;

            var generation = 0;
            var bestFitness = 0.0;
            DNA bestDna = null;

            var populationSize = 1000;
            var demandPower = 10000;
            var demandPowerCoef = 0.12;

            var population = PopulationGenerator.Generate(populationSize).ToList();

            while (generation < maxGeneration)
            {
                generation++;

                //Try to mutation
                foreach (var dna in population)
                    dna.Mutation();

                population = population.SelectByPowerDemand(demandPower, demandPowerCoef).ToList();
                Console.WriteLine($"Mid Population:{population.Count}");

                //Try to Corssover
                foreach (var _ in Enumerable.Range(0, populationSize))
                {
                    var loadRateParent = population.SelectByLoadRate(demandPower);
                    var costParent = population.SelectByCost(demandPower);
                    var co2Parent = population.SelectByCO2(demandPower);
                    var parents = new List<DNA>();
                    parents.Add(loadRateParent);
                    parents.Add(costParent);
                    parents.Add(co2Parent);

                    var parentIndex1 = Rand.Next(0, 3);
                    var parentIndex2 = Rand.Next(0, 3);
                    while (parentIndex1 == parentIndex2)
                        parentIndex2 = Rand.Next(0, 3);

                    var parent1 = parents[parentIndex1];
                    var parent2 = parents[parentIndex2];

                    var children = parent1.CrossOver(parent2);
                    if (children.Item1.GetTotalPower() >= demandPower)
                        population.Add(children.Item1);
                    if (children.Item2.GetTotalPower() >= demandPower)
                        population.Add(children.Item2);

                    if (population.Count > populationSize)
                        break;
                }

                population = population.OrderBy(x => x.Fitness(demandPower)).ToList();

                var lastDna = population.Last();

                if (lastDna.GetTotalPower() >= demandPower)
                {
                    var fitness = lastDna.Fitness(demandPower);
                    if (fitness > bestFitness)
                    {
                        bestFitness = fitness;
                        bestDna = lastDna.Copy();
                    }
                }

                //if (Math.Abs(lastBestfitNess - bestFitness) < 0.0001)
                //    break;


                Console.WriteLine(bestDna);
                Console.WriteLine($"Best fitness: {bestDna.Fitness(demandPower)}");
                Console.WriteLine($"Best Total Power: {bestDna.GetTotalPower()}");
                Console.WriteLine($"Best LoadRate: {bestDna.GetLoadRate(demandPower)}");
                Console.WriteLine($"Best Cost: {bestDna.GetAverageCost(demandPower)}");
                Console.WriteLine($"Best CO2: {bestDna.GetAverageCO2(demandPower)}");
                Console.WriteLine($"Gen: {generation}");
                Console.WriteLine($"Population Size: {population.SelectByPowerDemand(demandPower, demandPowerCoef).Count()}");
                Console.WriteLine($"=========================");

                if (Math.Abs(bestFitness - 1.1054208443867) < 0.0001)
                    break;
            }

            Console.ReadKey();
        }
    }
}
