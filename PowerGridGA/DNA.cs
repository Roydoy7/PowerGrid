using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PowerGridGA
{
    public class DNA : Collection<Gene>
    {
        public static int DNALength { get; private set; } = 6;
        private static Random Rand = new Random();
        public DNA(IEnumerable<Gene> genes)
        {
            foreach(var gene in genes)
            {
                this.Add(gene);
            }
        }

        public DNA Copy()
        {
            var genes = new List<Gene>();            
            foreach (var gene in this)
                genes.Add(gene.Copy());
            return new DNA(genes);
        }

        public void Mutation()
        {
            foreach (var _ in Enumerable.Range(0, 3))
            {
                var pos = Rand.Next(0, DNA.DNALength);
                this[pos].Mutation();
            }
        }

        public override string ToString()
        {
            var str = "";
            foreach (var gene in this)
                str += gene.ToString();
            return str;
        }
               
    }
}
