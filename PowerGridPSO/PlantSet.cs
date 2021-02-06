using System.Collections.ObjectModel;

namespace PowerGridPSO
{
    public class PlantSet : Collection<Plant>
    {
        public double FitnessLast { get; set; }
        public static double GlobalFitnessLast { get; set; }

        public PlantSet Copy()
        {
            var plantSet = new PlantSet();
            foreach (var p in this)
                plantSet.Add(p.Copy());
            return plantSet;
        }

        //Update personal best
        public void UpdatePBest(double fitness)
        {
            if (fitness > FitnessLast)
                foreach (var p in this)
                    p.UpdatePBest();            
        }

        //Update global best
        public void UpdateGBest(double fitness)
        {
            if (fitness > GlobalFitnessLast)
                foreach (var p in this)
                    p.UpdateGBest();
        }

        public void UpdateBestFitness(double fitness)
        {
            if (fitness > FitnessLast)
                FitnessLast = fitness;
            if (fitness > GlobalFitnessLast)
                GlobalFitnessLast = fitness;
        }

        public void NextPosition(double w)
        {
            foreach (var p in this)
                p.NextPosition(w);
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
