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

        public DNA Copy()
        {
            var dna = new DNA();
            foreach (var gene in this)
                dna.Add(gene.Copy());
            return dna;
        }
        public void AddRange(IEnumerable<Gene> genes)
        {
            foreach (var gene in genes)
                this.Add(gene);
        }

        public void Mutation()
        {
            foreach (var _ in Enumerable.Range(0, 6))
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
