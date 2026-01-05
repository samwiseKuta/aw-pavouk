using System;
using System.Collections.Generic;

namespace Models;

public class StartedCategory{

    public int Table {get;set;}

    public Bracket Bracket{get;set;}

    public StartedCategory(int table, Bracket bracket){
        Table = table;
        Bracket = bracket;
    }

    public override bool Equals(object? obj)
    {
        return obj is StartedCategory category &&
            Table == category.Table &&
            EqualityComparer<Bracket>.Default.Equals(Bracket, category.Bracket);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Table, Bracket);
    }
}
