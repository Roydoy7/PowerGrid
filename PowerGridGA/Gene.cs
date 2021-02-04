using System;

namespace PowerGridGA
{
    public class Gene
    {
        private static Random Rand { get; set; }
            = new Random();

        public Gene()
        {
        }
        public Gene(string code, int maxCount, double power, double cost, double co2)
        {
            Code = code;
            MaxCount = maxCount;
            Power = power;
            Cost = cost;
            CO2 = co2;
        }
        public Gene Copy()
        {
            return new Gene
            {
                Code = Code,
                MaxCount = MaxCount,
                Count = Count,
                Power = Power,
                Cost = Cost,
                CO2 = CO2
            };
        }

        public string Code { get; private set; }
        public int MaxCount { get; private set; }
        public int Count { get; set; }
        public double Power { get; private set; }
        public double Cost { get; private set; }
        public double CO2 { get; private set; }
        public Gene NextRandom()
        {
            return new Gene()
            {
                Code = Code,
                MaxCount = MaxCount,
                Count = Rand.Next(0, MaxCount + 1),
                Power = Power,
                Cost = Cost,
                CO2 = CO2,
            };
        }

        public void Mutation()
        {
            //Count = Rand.Next(0, MaxCount + 1);

            var posOrNega = Rand.Next(0, 2) % 2;
            var mutationStep = Rand.Next(0, 4);

            if (posOrNega == 0)
                Count += mutationStep;
            else
                Count -= mutationStep;

            if (Count > MaxCount)
                Count = MaxCount;
            if (Count < 0)
                Count = 0;
        }

        public override string ToString()
        {
            return string.Format($"{Code}{Count} ");
        }
    }
}
