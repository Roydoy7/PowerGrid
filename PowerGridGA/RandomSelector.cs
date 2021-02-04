using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerGridGA
{
    public class RandomSelector
    {
        private static Random Rand = new Random();
        public static int RandomSelect(List<double> probabilities)
        {
            var dict = probabilities.Where(x => x > 0).Select(x=> new KeyValuePair<int, double>(probabilities.IndexOf(x), x)).ToList();

            var value = Rand.NextDouble();
            var cumulate = 0.0;
            for (var i = 0; i < dict.Count(); i++)
            {
                cumulate += dict[i].Value;
                if (cumulate > value)
                    return dict[i].Key;
            }

            return 0;
        }


    }
}
