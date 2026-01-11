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
        public int? Id {get;set;}
        public Competitor? CompetitorOne;
        public Competitor? CompetitorTwo;
        public Node? Up {get;set;}
        public Node? Left {get;set;}
        public Node? Right {get;set;}

        public override string? ToString()
        {
            return $"{this.CompetitorOne?.ToString() ?? "---"} |VS| {this.CompetitorTwo?.ToString()??"---"}";
        }

        public bool IsPopulated(){
            return CompetitorOne is not null || CompetitorTwo is not null;
        }
        public bool IsFull(){
            return CompetitorOne is not null && CompetitorTwo is not null;
        }

        public bool IsLeaf(){
            return (Left is null && Right is null);
        }
    }

    private Competitor? RemoveRandomCompetitor(List<Competitor> pickingFrom){
        if(pickingFrom.Count == 0) return null;
        Random rng = new Random();
        int i = rng.Next(pickingFrom.Count);
        Competitor competitor = pickingFrom[i];
        pickingFrom.RemoveAt(i);
        return competitor;
    }



    public void GenerateMatchesTest(){
        GenerateEmptyBracket();
        RandomlyPopulateBracket();

    }


    public void RandomlyPopulateBracket(){
        int maxDepth = GetMaxDepth();

        var queue = new Queue<Node>();
        queue.Enqueue(Root);

        int currentDepth = 0;

        List<Competitor> compCopy = new(Competitors);

        while (queue.Count > 0)
        {
            int levelCount = queue.Count;
            currentDepth++;

            Console.WriteLine("Current level count:");
            Console.WriteLine(levelCount);

            if(currentDepth == maxDepth-1){
                while(queue.Count > 0){
                    Node curr = queue.Dequeue();
                    if(curr.Left is not null){
                        curr.Left.CompetitorOne=RemoveRandomCompetitor(compCopy);
                        curr.Left.CompetitorTwo=RemoveRandomCompetitor(compCopy);
                    }
                    if(curr.Right is not null){
                        curr.Right.CompetitorOne=RemoveRandomCompetitor(compCopy);
                        curr.Right.CompetitorTwo=RemoveRandomCompetitor(compCopy);
                    }

                    if(curr.Right is null || curr.Left is null){
                        curr.CompetitorOne = RemoveRandomCompetitor(compCopy);
                    }
                }
                Console.WriteLine("Leftovers:");
                Console.WriteLine(compCopy.Count);
                return;
            }

            
            for (int i = 0; i < levelCount; i++)
            {
                var node = queue.Dequeue();

                if (node.Left != null)
                    queue.Enqueue(node.Left);
                if (node.Right != null)
                    queue.Enqueue(node.Right);
            }
        }

    }

    public void GenerateEmptyBracket(){
        Queue<Node> unlinkedQ = new();
        Node root = new Node(){Id=0};
        this.Root = root;
        unlinkedQ.Enqueue(root);
        int requiredNodes = (Competitors.Count-1);
        int i = 1;
        while(i<requiredNodes){
            Node curr = unlinkedQ.Dequeue();
            if(i < requiredNodes){
                curr.Left = new Node(){Id=i,Up=curr};
                unlinkedQ.Enqueue(curr.Left);
                i++;
            }
            if(i < requiredNodes){
                curr.Right= new Node(){Id=i,Up=curr};
                unlinkedQ.Enqueue(curr.Right);
                i++;
            }
        }
    }


    public string ToTreeNumberString(){
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
            value+=level.Count;
            //foreach(Node node in level){
            //    value += "  "+node.ToString();
            //}
            value+=" \n\n";
        }

        return value;
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
                value += "  "+node.ToString();
            }
            value+=" \n\n";
        }

        return value;
    }


int GetMaxDepth()
{
    if (this.Root == null)
        return 0;

    var queue = new Queue<Node>();
    queue.Enqueue(Root);

    int height = 0;

    while (queue.Count > 0)
    {
        int levelCount = queue.Count;
        height++;

        for (int i = 0; i < levelCount; i++)
        {
            var node = queue.Dequeue();

            if (node.Left != null)
                queue.Enqueue(node.Left);
            if (node.Right != null)
                queue.Enqueue(node.Right);
        }
    }

    return height;
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
