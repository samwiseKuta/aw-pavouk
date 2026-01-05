using System;
using System.Collections;
using System.Collections.Generic;

namespace Models;
public class Bracket : IComparable<Bracket>, IEquatable<Bracket>
{


    public Node? Root;
    public string Name {get;set;}
    public string Elimination {get;set;}
    public int Depth {get;set;}
    public int NodeCount {get;set;}
    public enum Type {
        SingleElimination,
        DoubleElimination
    }
    public List<Competitor> Competitors{get;set;}
    public class Node
    {
        public Competitor? CompetitorOne;
        public Competitor? CompetitorTwo;
        public Node? Left {get;set;}
        public Node? Right {get;set;}

        public override string? ToString()
        {
            return $"{this.CompetitorOne?.ToString() ?? "---"} VS {this.CompetitorTwo?.ToString()??"---"}";
        }
    }


    public void GenerateMatches(){

        List<Competitor> compCopy = new List<Competitor>(Competitors);

        for(int i = 1; i < Competitors.Count; i++){
            Competitor? compOne = PickCompetitor(compCopy);
            Competitor? compTwo = PickCompetitor(compCopy);

            this.AddLeaf(new Node(){
                    CompetitorOne = compOne,
                    CompetitorTwo = compTwo
                    });
        }
    }


    private Competitor? PickCompetitor(List<Competitor> pickingFrom){
        if(pickingFrom.Count == 0) return null;
        Random rng = new Random();
        int i = rng.Next(pickingFrom.Count);
        Console.WriteLine("Picking from size:");
        Console.WriteLine(pickingFrom.Count);
        Console.WriteLine("Index:");
        Console.WriteLine(i);
        Competitor competitor = pickingFrom[i];
        pickingFrom.RemoveAt(i);
        return competitor;
    }

    public void GenerateEmptyBracket(int participantsCount) {

        for(int i = 1; i < participantsCount; i++){
            this.AddLeaf(new Node());
        }
    }

    public void GenerateEmptyBracket(){

        for(int i = 1; i < this.Competitors.Count; i++){
            this.AddLeaf(new Node());
        }
    }


    public void AddLeaf(Node leafNode){

        if(this.Root is null) {
            this.Root = leafNode;
            this.NodeCount = 1;
            this.Depth = 0;
            return;
        }
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(this.Root);
        
        while(true){
            Node curr = queue.Dequeue();
            if(curr.Left is null){

                curr.Left = leafNode;
                return;
            }

            if(curr.Right is null){
                curr.Right = leafNode;
                return;
            }

            queue.Enqueue(curr.Left);
            queue.Enqueue(curr.Right);
        }
    }

    public string ToTreeString(){
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(this.Root!);
        List<List<Node>> levels = new List<List<Node>>();
        int depth = 0;
        while(true){
            if(queue.Count == 0) break;

            levels.Add(new List<Node>());
            int currentDepthCount = queue.Count;
            for(int i = 0; i < currentDepthCount; i++){
                Node curr = queue.Dequeue();
                levels[depth].Add(curr);
                if(curr.Left is not null) queue.Enqueue(curr.Left);
                if(curr.Right is not null) queue.Enqueue(curr.Right);
            }
            depth++;
            
        }

        string value = "";

        foreach(List<Node> level in levels){
            foreach(Node node in level){
                value += ""+node.ToString();
            }
            value+=" \n\n";
        }

        return value;
    }

    public override string? ToString()
    {
        return $"{this.Name}";
    }


    public int CompareTo(Bracket? other)
    {
        if(other is null) return -1;

        return other.Name.CompareTo(this.Name);
    }

    public override bool Equals(object? obj)
    {
        return this.GetHashCode() == obj?.GetHashCode();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name,Competitors.Count,Elimination);
    }

    public bool Equals(Bracket? other)
    {
        return GetHashCode() == other?.GetHashCode();
    }
}
