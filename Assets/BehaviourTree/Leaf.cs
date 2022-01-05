using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    public delegate Status Tick();
    public Tick ProcessMethob;

    public Leaf() { }

    public Leaf(string name,Tick pm)
    {
        this.name = name;
        ProcessMethob = pm;
    }   

    public override Status Process()
    {
        if(ProcessMethob!=null)
            return ProcessMethob();
        return Status.FAILURE;
    }
}
