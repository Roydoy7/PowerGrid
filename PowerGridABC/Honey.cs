using System;

namespace PowerGridABC
{
    public class Honey
    {
        private static Random Rand { get; set; }
            = new Random();

        public Honey()
        {
        }
        public Honey(string code, int maxCount, double power, double cost, double co2)
        {
            Code = code;
            MaxCount = maxCount;
            Power = power;
            Cost = cost;
            CO2 = co2;
        }
        public Honey Copy()
        {
            return new Honey
            {
                Code = Code,
                MaxCount = MaxCount,
                Count = Count,
                Power = Power,
                Cost = Cost,
                CO2 = CO2,
            };
        }

        public string Code { get; private set; }
        public int Count { get; set; }
        public int MaxCount { get; private set; }
        public double Power { get; private set; }
        public double Cost { get; private set; }
        public double CO2 { get; private set; }

        public Honey NextRandom()
        {
            return new Honey()
            {
                Code = Code,
                MaxCount = MaxCount,
                Count = Rand.Next(0, MaxCount + 1),
                Power = Power,
                Cost = Cost,
                CO2 = CO2,
            };
        }

        public void SetCount(int count)
        {
            Count = count;
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
