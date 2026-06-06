using System.IO;

namespace Brachistochrone_Calc_Mk2;

internal static class FileManager
{
    public static ObjectDatabase LoadObjects(string path)
    {
        ObjectDatabase db = new ObjectDatabase();

        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split(',');

            string name = parts[0];

            double avg = double.Parse(parts[1]);
            double min = double.Parse(parts[2]);
            double max = double.Parse(parts[3]);

            db.Add(new Planet(name, avg, min, max));
        }

        return db;
    }
}