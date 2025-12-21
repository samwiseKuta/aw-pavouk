using System.Collections.Generic;

namespace Models;
class Bracket
{


    public required string name;
    public int depth;
    public int count;
    public enum type{
        SingleElimination,
        DoubleElimination
    }
    public class Node
    {
        public int number;
        public Node? left;
        public Node? right;

        public Node(int number){
            this.number = number;
        }

        public override string? ToString()
        {
            return ""+this.number;
        }
    }
    private Node? root;

    public Bracket GenerateEmpty(int participantsCount) {

        for(int i = 1; i < participantsCount; i++){
            this.AddLeaf(new Node(i));
        }

        return this;
    }

    public Bracket GenerateEmpty(List<string> participants){

        return this;
    }


    public void AddLeaf(Node leafNode){

        if(this.root is null) {
            this.root = leafNode;
            this.count = 1;
            this.depth = 0;
            return;
        }
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(this.root);
        
        while(true){
            Node curr = queue.Dequeue();
            if(curr.left is null){

                curr.left = leafNode;
                return;
            }

            if(curr.right is null){
                curr.right = leafNode;
                return;
            }

            queue.Enqueue(curr.left);
            queue.Enqueue(curr.right);
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
                if(curr.left is not null) queue.Enqueue(curr.left);
                if(curr.right is not null) queue.Enqueue(curr.right);
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
}
