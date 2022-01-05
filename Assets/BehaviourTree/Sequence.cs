using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
   public Sequence(string name)
   {
        this.name = name;
   }

    public Sequence()
    {

    }

    public override Status Process()
    {
        Status childstatust = children[currentChild].Process();
        Debug.Log(childstatust);
        Debug.Log(currentChild);
        if (childstatust == Status.RUNNING)
            return Status.RUNNING;
        if (childstatust == Status.FAILURE)
            return childstatust;
        currentChild++;
        if(currentChild>=children.Count){
            currentChild = 0;
            return Status.SUCCESS;
        }
        return Status.RUNNING;
    }
}
