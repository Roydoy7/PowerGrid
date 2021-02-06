using System;
using System.Collections.ObjectModel;

namespace PowerGridABC
{
    public class Bee : Collection<Honey>
    {
        private static Random Rand = new Random();
        public double FitnessLast { get; set; }
        public int ReSearchLimit { get; private set; } = 100;
        public int ReSearchCount { get; private set; } = 0;

        public Bee Copy()
        {
            var workerBee = new Bee
            {
                FitnessLast = this.FitnessLast                
            };

            foreach (var h in this)
                workerBee.Add(h.Copy());
            return workerBee;
        }

        public bool IsReSearchLimitExceed()
        {
            //If the research count exceed the limit
            //Generate a new bee
            //This is turning self into a scout bee
            if (ReSearchCount > ReSearchLimit)
            {
                this.Clear();
                foreach (var h in BeeGenerator.Generate())
                    this.Add(h);
                //Reset count
                ReSearchCount = 0;
                return true;
            }
            return false;
        }

        public void ReSearch(Bee bee, double demandPower)
        {           
            //Dimension
            var d = this.Count;
            //Random k
            var k = Rand.Next(0, d);
            //Random number between -1 and 1
            var rand = Rand.NextDouble() * Math.Pow((-1), Rand.Next(0, 2));
            //Copy self as a newBee
            var newBee = this.Copy();
            //Get the k-th honey
            var honeyK = this[k];
            //Try to set the honeyK's count to a new count
            newBee[k].SetCount((int)Math.Round(honeyK.Count + rand * (honeyK.Count - bee[k].Count)));
            //If the newBee's fitness is better than self
            var newFitness = newBee.Fitness(demandPower);
            FitnessLast = this.Fitness(demandPower);
            if (newFitness > FitnessLast)
            {
                //Use the newBee's count
                this[k].SetCount(newBee[k].Count);
                FitnessLast = newFitness;
            }
            //Research count++
            else
                ReSearchCount++;
        }

        public override string ToString()
        {
            var str = "";
            foreach (var p in this)
                str += p.ToString();
            return str;
        }
    }
}
