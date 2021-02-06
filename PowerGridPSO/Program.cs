using System;
using System.Diagnostics;
using System.Linq;

namespace PowerGridPSO
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var maxGeneration = 2000;
            var generation = 0;
            var bestFitness = 0.0;
            PlantSet bestPlantSet = null;

            var populationSize = 1000;
            var demandPower = 10000;

            //Init population
            var population = PopulationGenerator.Generate(populationSize).ToList();
            while(generation<maxGeneration)
            {
                //For each partical update pbest and gbest
                foreach(var p in population)
                {
                    var fitness = p.Fitness(demandPower);
                    p.UpdatePBest(fitness);
                    p.UpdateGBest(fitness);
                    p.UpdateBestFitness(fitness);
                }

                //Get the best one
                population = population.OrderBy(x => x.FitnessLast).ToList();
                var lastP = population.Last();

                //Save the best one
                if(lastP.GetTotalPower()>=demandPower)
                {
                    var fitness = lastP.FitnessLast;
                    if(fitness>bestFitness)
                    {
                        bestFitness = fitness;
                        bestPlantSet = lastP.Copy();
                    }
                }
                               
                Console.WriteLine(bestPlantSet);
                Console.WriteLine($"Best fitness: {bestPlantSet.Fitness(demandPower)}");
                Console.WriteLine($"Best Total Power: {bestPlantSet.GetTotalPower()}");
                Console.WriteLine($"Best LoadRate: {bestPlantSet.GetLoadRate(demandPower)}");
                Console.WriteLine($"Best Cost: {bestPlantSet.GetAverageCost(demandPower)}");
                Console.WriteLine($"Best CO2: {bestPlantSet.GetAverageCO2(demandPower)}");
                Console.WriteLine($"Gen: {generation}");
                Console.WriteLine($"=========================");

                //This the known best solution.
                if (Math.Abs(bestFitness - 1.1054208443867) < 0.0001)
                    break;

                //Move partical
                var w = WCalculator.CalcW(generation, maxGeneration);
                foreach (var p in population)
                    p.NextPosition(w);
                generation++;
            }

            stopWatch.Stop();
            Console.WriteLine($"Execution Time:{stopWatch.Elapsed.TotalSeconds}");
            Console.ReadKey();
        }
    }
}
