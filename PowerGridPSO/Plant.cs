using System;

namespace PowerGridPSO
{
    public class Plant
    {
        private static Random Rand { get; set; }
            = new Random();
        public Plant()
        {
        }
        public Plant(string code, int maxCount, double power, double cost, double co2)
        {
            Code = code;
            MaxCount = maxCount;
            Power = power;
            Cost = cost;
            CO2 = co2;
        }
        public Plant Copy()
        {
            return new Plant
            {
                Code = Code,
                MaxCount = MaxCount,
                Count = Count,
                Power = Power,
                Cost = Cost,
                CO2 = CO2,
                PBest=PBest,
                V=V,
            };
        }
        public string Code { get; private set; }
        public int Count { get; set; }
        public int MaxCount { get; private set; }
        public double Power { get; private set; }
        public double Cost { get; private set; }
        public double CO2 { get; private set; }
        private double V { get; set; } = 0.0;
        public int PBest { get; set; }
        public Plant NextRandom()
        {
            return new Plant()
            {
                Code = Code,
                MaxCount = MaxCount,
                Count = Rand.Next(0, MaxCount + 1),
                Power = Power,
                Cost = Cost,
                CO2 = CO2,
            };
        }
        public void NextPosition(double w, double c1 = 2, double c2 = 2)
        {
            var rand = Rand.NextDouble();
            var gbest = PlantGBest.GetGBest(Code);
            V = Math.Round(w * V + c1 * rand * (PBest - Count) + c2 * rand * (gbest - Count));
            Count += Convert.ToInt32(V);
            if (Count > MaxCount)
                Count = MaxCount;
            if (Count < 0)
                Count = 0;
        }        
        public void UpdatePBest()
        {
            PBest = Count;
        }
        public void UpdateGBest()
        {
            PlantGBest.SetGBest(Code, Count);
        }
        public override string ToString()
        {
            return string.Format($"{Code}{Count} ");
        }
    }
}
