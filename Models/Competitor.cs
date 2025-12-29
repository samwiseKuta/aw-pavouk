namespace Models;

public class Competitor
{

    public string Name {get;set;}
    public float Weight {get;set;}
    public string? Thumbnail {get;set;}
    public string? Origin {get;set;}

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string? ToString()
    {
        return this.Name + " | " + Weight + " | " +Origin;
    }
}
