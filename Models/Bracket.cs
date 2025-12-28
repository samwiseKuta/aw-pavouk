using System;
using System.Collections;
using System.Collections.Generic;

namespace Models;
public class Bracket : IComparable<Bracket>
{


    private Node? root;
    public string Name {get;set;}
    public int Depth {get;set;}
    public int Count {get;set;}
    public enum type{
        SingleElimination,
        DoubleElimination
    }
    public List<Competitor> Competitors{get;set;}
    public class Node
    {
        public Competitor Competitor;
        public Node? Left {get;set;}
        public Node? Right {get;set;}

        public Node(Competitor competitor){
            this.Competitor = competitor;

        }

        public override string? ToString()
        {
            return this.Competitor.ToString();
        }
    }


    public Bracket(){

    }

    public Bracket GenerateEmpty(int participantsCount) {

        for(int i = 1; i < participantsCount; i++){
            this.AddLeaf(new Node(new Competitor()));
        }

        return this;
    }

    public Bracket GenerateEmpty(List<string> participants){

        return this;
    }


    public void AddLeaf(Node leafNode){

        if(this.root is null) {
            this.root = leafNode;
            this.Count = 1;
            this.Depth = 0;
            return;
        }
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(this.root);
        
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

    public override string? ToString()
    {

        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(this.root!);
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
                value += "| "+node.ToString();
            }
            value+=" |\n";
        }

        return value;
    }


    public int CompareTo(Bracket? other)
    {
        if(other is null) return -1;

        return other.Name.CompareTo(this.Name);
    }
}
