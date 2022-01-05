using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobberBehaviour : MonoBehaviour
{
    BehaviourTree tree;
    public GameObject diamond;
    public GameObject van;
    public GameObject backDoor;
    public GameObject frontDoor;
    NavMeshAgent agent;
    
    public enum ActionState { IDLE, WORKING};
    ActionState state = ActionState.IDLE;
    Node.Status treeStatus = Node.Status.RUNNING;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        tree = new BehaviourTree();
        Sequence steal = new Sequence("Steal Something");
        Leaf goToFrontDoor = new Leaf("Go To FrontDoor", GoToFrontDoor);
        Leaf goToBackDoor = new Leaf("Go to backdoor", GoToBackDoor);
        Leaf goToDiamond = new Leaf("Go To Diamond",GoToDiamond);
        Leaf goToVan = new Leaf("Go To Van",GoToVan);
        Selector opendoor = new Selector("Open Door");

        opendoor.AddChild(goToFrontDoor);
        opendoor.AddChild(goToBackDoor);

        steal.AddChild(opendoor);
        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);
        tree.AddChild(steal);

        tree.PrintTree();
    }

    private Node.Status GoToFrontDoor()
    {
        return GoToDoor(frontDoor);
    }

    private Node.Status GoToBackDoor()
    {
        return GoToDoor(backDoor);
    }

    public Node.Status GoToDiamond()
    {
        return StealSomething(diamond);
    }

    public Node.Status GoToVan()
    {
        return GoToLocation(van.transform.position);
    }

    public Node.Status GoToDoor(GameObject door)
    {
        Node.Status s = GoToLocation(door.transform.position);
        if (s == Node.Status.SUCCESS)
        {
            if (!door.GetComponent<Lock>().isLocked)
            {
                door.SetActive(false);
                return Node.Status.SUCCESS;
            }
            return Node.Status.FAILURE;
        }
        else
            return s;
    }

    public Node.Status StealSomething(GameObject stuff)
    {
        Node.Status s = GoToLocation(stuff.transform.position);
        if(s == Node.Status.SUCCESS)
        {
            //stuff.SetActive(false);
            stuff.transform.parent = this.gameObject.transform;
        }
        return s;
    }

    Node.Status GoToLocation(Vector3 destination)
    {
        float distanceToTarget = Vector3.Distance(destination, this.transform.position);
        Debug.Log(distanceToTarget);
        if (state == ActionState.IDLE)
        {
            agent.SetDestination(destination);
            state = ActionState.WORKING;
        }else if(Vector3.Distance(agent.pathEndPosition, destination) >= 2)
        {
            state = ActionState.IDLE;
            return Node.Status.FAILURE;
        }else if(distanceToTarget < 2)
        {
            state = ActionState.IDLE;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
   }

    // Update is called once per frame
    void Update()
    {
        if(treeStatus==Node.Status.RUNNING)
            treeStatus = tree.Process();
    }
}
