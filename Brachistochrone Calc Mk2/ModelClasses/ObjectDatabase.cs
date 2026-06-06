namespace Brachistochrone_Calc_Mk2;

internal class ObjectDatabase
{
    public List<Planet> Objects { get; set; }

    public ObjectDatabase()
    {
        Objects = new List<Planet>();
    }

    public void Add(Planet obj)
    {
        Objects.Add(obj);
    }

    public override string ToString()
    {
        return $"there is {Objects.Count} objects right now, in the database.";
    }
}