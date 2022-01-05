using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public enum Status { SUCCESS, RUNNING, FAILURE };
    public Status status;
    public string name;
    public List<Node> children = new List<Node>();
    public int currentChild = 0;

    public void AddChild(Node node)
    {
        children.Add(node);
    }

    public virtual Status Process()
    {
        return children[currentChild].Process();
    }

    public Node(){}

    public Node(string name)
    {
        this.name = name;
    }
}
