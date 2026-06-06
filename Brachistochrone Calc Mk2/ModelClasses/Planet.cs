namespace Brachistochrone_Calc_Mk2;

public class Planet
{
    public string Name { get; set; }

    public DistanceRange Distances { get; set; }

    public Planet(string name, double avg, double min, double max)
    {
        Name = name;

        Distances = new DistanceRange
        {
            Avg = avg,
            Min = min,
            Max = max
        };
    }

    public override string ToString()
    {
        return $"{Name} | avg {Distances.Avg} km";
    }
}