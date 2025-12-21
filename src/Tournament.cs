namespace App;

using System;
using System.Collections.Generic;

public class Tournament
{
    public required string Name {get;set;}
    public required string Location {get;set;}
    public DateTime Date {get;set;}
    List<Bracket>? brackets;
    List<int>? tables;


    public Tournament(){

    }



}
