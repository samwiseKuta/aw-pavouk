using System;

namespace Models;

public class Competitor : IEquatable<Competitor>,IComparable<Competitor>
{

    public string Name {get;set;}
    public float Weight {get;set;}
    public string? Thumbnail {get;set;}
    public string? Origin {get;set;}

    public int CompareTo(Competitor? other)
    {
        return this.Name.CompareTo(other.Name);
    }

    public override bool Equals(object? obj)
    {
        if(obj is null) return false;
        return this.GetHashCode() == obj?.GetHashCode();
    }

    public bool Equals(Competitor? other)
    {
        return this.GetHashCode() == other.GetHashCode();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name,Weight,Origin);
    }

    public override string? ToString()
    {
        return this.Name + " | " + Weight + " | " +Origin;
    }
}
