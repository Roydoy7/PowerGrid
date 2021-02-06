using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PowerGridGA
{
    class Program
    {
        private static Random Rand = new Random();
        static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

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
                    //Select parent by load rate
                    var loadRateParent = population.SelectByLoadRate(demandPower);
                    //Select parent by cost
                    var costParent = population.SelectByCost(demandPower);
                    //Select parent by co2 emission
                    var co2Parent = population.SelectByCO2(demandPower);
                    //Add parents into a collection
                    var parents = new List<DNA>();                    
                    parents.Add(loadRateParent);
                    parents.Add(costParent);
                    parents.Add(co2Parent);

                    //Random select two from three
                    var parentIndex1 = Rand.Next(0, 3);
                    var parentIndex2 = Rand.Next(0, 3);
                    while (parentIndex1 == parentIndex2)
                        parentIndex2 = Rand.Next(0, 3);

                    var parent1 = parents[parentIndex1];
                    var parent2 = parents[parentIndex2];

                    //Cross over using the selected parents
                    var children = parent1.CrossOver(parent2);
                    //Add new blood into current population
                    if (children.Item1.GetTotalPower() >= demandPower)
                        population.Add(children.Item1);
                    if (children.Item2.GetTotalPower() >= demandPower)
                        population.Add(children.Item2);

                    //Stop if population exceeds the limit
                    if (population.Count > populationSize)
                        break;
                }

                //Order to get the best
                population = population.OrderBy(x => x.Fitness(demandPower)).ToList();
                //This is the best, for now
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

                Console.WriteLine(bestDna);
                Console.WriteLine($"Best fitness: {bestDna.Fitness(demandPower)}");
                Console.WriteLine($"Best Total Power: {bestDna.GetTotalPower()}");
                Console.WriteLine($"Best LoadRate: {bestDna.GetLoadRate(demandPower)}");
                Console.WriteLine($"Best Cost: {bestDna.GetAverageCost(demandPower)}");
                Console.WriteLine($"Best CO2: {bestDna.GetAverageCO2(demandPower)}");
                Console.WriteLine($"Gen: {generation}");
                Console.WriteLine($"Population Size: {population.SelectByPowerDemand(demandPower, demandPowerCoef).Count()}");
                Console.WriteLine($"=========================");

                //This the known best solution.
                if (Math.Abs(bestFitness - 1.1054208443867) < 0.0001)
                    break;
            }

            stopWatch.Stop();
            Console.WriteLine($"Execution Time:{stopWatch.Elapsed.TotalSeconds}");
            Console.ReadKey();
        }
    }
}
