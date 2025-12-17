namespace App;

using System;
using System.Collections.Generic;

public class Tournament
{
    public required string name;
    public required string location;
    public DateTime date;
    List<Bracket>? brackets;
    List<int>? tables;
}
