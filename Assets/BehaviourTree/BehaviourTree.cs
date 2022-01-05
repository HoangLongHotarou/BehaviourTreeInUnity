using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : Node
{
    public BehaviourTree()
    {
        this.name = "Tree";
    }

    public BehaviourTree(string name)
    {
        this.name = name;
    }

    public override Status Process()
    {
        return children[currentChild].Process();
    }

    public struct NodeLevel
    {
        public int level;
        public Node node;
    }

    // print Not use recusion => best match
    public void PrintTree()
    {
        string treePrintOut = "";
        Stack nodeStack = new Stack();
        nodeStack.Push(new NodeLevel { level = 0, node = this });
        while (nodeStack.Count != 0)
        {
            NodeLevel nl = (NodeLevel)nodeStack.Pop();
            treePrintOut += new string('-', nl.level)+nl.node.name+"\n";
            for (int i = nl.node.children.Count-1; i >=0 ; i--)
            {
                nodeStack.Push(new NodeLevel { level = nl.level+1, node = nl.node.children[i] });
            }
        }
        Debug.Log(treePrintOut);
    }

    //print use recusion
    public void RecusionTree(ref string n, NodeLevel nl)
    {
        n += new string('-', nl.level)+nl.node.name+"\n";
        for (int i = 0; i < nl.node.children.Count; i++)
        {
            RecusionTree(ref n,new NodeLevel { level = nl.level+1,node = nl.node.children[i] });
        }
    }

    public void PrintTreeRcusion()
    {
        string n = "";
        RecusionTree(ref n,new NodeLevel { level = 0,node =this });
        Debug.Log(n);
    }
}
