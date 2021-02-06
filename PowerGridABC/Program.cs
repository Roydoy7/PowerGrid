using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PowerGridABC
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
            Bee bestBee = null;

            var populationSizeEmployee = 800;
            var populationSizeOnlooker = 200;
            var demandPower = 10000;

            //Init employee bee population
            var populationEmployeeBee = PopulationGenerator.Generate(populationSizeEmployee).ToList();
            //Onlooker bee collection
            var populationOnlookerBee = new List<Bee>();
            while (generation < maxGeneration)
            {
                //For each employee bee
                foreach (var b in populationEmployeeBee)
                {
                    //Grab a random other employee and perform a close search
                    var k = Rand.Next(0, populationSizeEmployee);
                    var beeK = populationEmployeeBee[k];
                    //Check if this bee has exceed search limit
                    //If so this function will turn this bee into a scout bee,
                    //which is, to generate a new bee.
                    if (!b.IsReSearchLimitExceed())
                    {
                        //If not, perform a close search
                        b.ReSearch(beeK, demandPower);
                    }
                }

                //Clear onlooker bee's collection
                populationOnlookerBee.Clear();
                //Get bees from employee collcetion using Roulette selection
                foreach (var _ in Enumerable.Range(0, populationSizeOnlooker))
                    populationOnlookerBee.Add(populationEmployeeBee.RouletteSelect().Copy());

                //For each onlooker bee, perform a close search
                foreach (var b in populationOnlookerBee)
                {
                    //Grab a random bee from onlooker bee's collection
                    var k = Rand.Next(0, populationSizeOnlooker);
                    var beeK = populationOnlookerBee[k];
                    //Perform close search
                    b.ReSearch(beeK, demandPower);
                }

                //Get the best one, for now
                populationOnlookerBee = populationOnlookerBee.OrderBy(x => x.FitnessLast).ToList();
                var lastBee = populationOnlookerBee.Last();

                //Save the best one
                if (lastBee.GetTotalPower() >= demandPower)
                {
                    var fitness = lastBee.FitnessLast;
                    if (fitness > bestFitness)
                    {
                        bestFitness = fitness;
                        bestBee = lastBee.Copy();
                    }
                }

                Console.WriteLine(bestBee);
                Console.WriteLine($"Best fitness: {bestBee.Fitness(demandPower)}");
                Console.WriteLine($"Best Total Power: {bestBee.GetTotalPower()}");
                Console.WriteLine($"Best LoadRate: {bestBee.GetLoadRate(demandPower)}");
                Console.WriteLine($"Best Cost: {bestBee.GetAverageCost(demandPower)}");
                Console.WriteLine($"Best CO2: {bestBee.GetAverageCO2(demandPower)}");
                Console.WriteLine($"Gen: {generation}");
                Console.WriteLine($"=========================");

                //This the known best solution.
                if (Math.Abs(bestFitness - 1.1054208443867) < 0.0001)
                    break;

                generation++;
            }

            stopWatch.Stop();
            Console.WriteLine($"Execution Time:{stopWatch.Elapsed.TotalSeconds}");
            Console.ReadKey();
        }
    }
}
