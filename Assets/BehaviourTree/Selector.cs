using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    public Selector(string name)
    {
        this.name = name;
    }

    public Selector()
    {

    }

    public override Status Process()
    {
        Status status = children[currentChild].Process();
        Debug.Log(currentChild);
        if(status == Status.RUNNING)return Status.RUNNING;
        if (status == Status.SUCCESS)
        {
            currentChild = 0;
            return Status.SUCCESS;
        }
        currentChild += 1;
        if (currentChild >= children.Count)
        {
            currentChild=0;
            return Status.FAILURE;
        }
        return Status.RUNNING;
    }
}
