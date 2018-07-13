using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DirectedAgent : MonoBehaviour
{

    private NavMeshAgent agent;

    // Use this for initialization
    void Awake()
    {
        agent = GetComponent< NavMeshAgent>();
        
    }

    public void Update()
    {
        var g = GameObject.Find("Player");
        MoveToLocation(g.transform.position);
    }

    public void MoveToLocation(Vector3 targetPoint)
    {
        agent.destination = targetPoint;
        agent.isStopped = false;
    }
}