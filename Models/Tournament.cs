namespace Models;

using System;
using System.Collections.Generic;

public class Tournament : IEquatable<Tournament>
{

    public required string Name {get;set;}
    public required string Location {get;set;}
    public DateTime Date {get;set;}
    List<Bracket>? brackets;
    List<int>? tables;


    public Tournament(){

    }

    public override string? ToString()
    {
        return $"{this.Name} | {this.Location} | {this.Date}";
    }

    public bool Equals(Tournament? other)
    {
        if (other is null) return false;
        return this.GetHashCode() == other.GetHashCode();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Location, Date);
    }
}
