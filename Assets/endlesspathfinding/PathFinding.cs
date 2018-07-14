using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Node: IComparable<Node>
{
    public Node(int x, int y, int cost)
    {
        this.x = x;
        this.y = y;
        this.cost = cost;
    }

    public int x, y, cost;

    public int CompareTo(Node obj)
    {
        return this.cost - obj.cost;
    }
}

public class PathFinding : MonoBehaviour {
    static int size = 100;
    private int[,] pathlen = new int[size, size];
    private bool[,] walkable = new bool[size, size];
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void runFirst(int x, int z)
    {
        for(var i=0; i<size;i++)
        {
            for (var j = 0; j< size; j++)
            {
                pathlen[i, j] = int.MaxValue;
            }
        }


        pathlen[x, z] = 0;
        PriorityQueue< Node> q = new PriorityQueue<Node>();
        while(q.Count > 0)
        {
            var n = q.Dequeue();
            if(n.cost < pathlen[x,z])
            {
                if (walkable[x, z])
                {
                    pathlen[x, z] = n.cost;
                    q.Enqueue(new Node(n.x - 1, n.y, n.cost + 1));
                    q.Enqueue(new Node(n.x + 1, n.y, n.cost + 1));
                    q.Enqueue(new Node(n.x, n.y - 1, n.cost + 1));
                    q.Enqueue(new Node(n.x, n.y + 1, n.cost + 1));
                }
            }
        }
    }

    public void place(int x, int z)
    {
        walkable[x, z] = false;
        
        Queue<Node> toremove = new Queue<Node>();
        toremove.Enqueue(new Node(x+1, z, pathlen[x, z] + 1));
        toremove.Enqueue(new Node(x-1, z, pathlen[x, z] + 1));
        toremove.Enqueue(new Node(x, z-1, pathlen[x, z] + 1));
        toremove.Enqueue(new Node(x, z+1, pathlen[x, z] + 1));

        pathlen[x, z] = int.MaxValue;
        PriorityQueue<Node> q = new PriorityQueue<Node>();

        while (toremove.Count > 0)
        {
            var n = toremove.Dequeue();
            if(pathlen[n.x, n.y] < n.cost)
            {
                q.Enqueue(new Node(n.x+1, n.y, pathlen[n.x, n.y]+1));
                q.Enqueue(new Node(n.x-1, n.y, pathlen[n.x, n.y] + 1));
                q.Enqueue(new Node(n.x, n.y+1, pathlen[n.x, n.y] + 1));
                q.Enqueue(new Node(n.x, n.y-1, pathlen[n.x, n.y] + 1));
            }
            else
            {
                toremove.Enqueue(new Node(n.x + 1, n.y, n.cost + 1));
                toremove.Enqueue(new Node(n.x - 1, n.y, n.cost + 1));
                toremove.Enqueue(new Node(n.x, n.y - 1, n.cost + 1));
                toremove.Enqueue(new Node(n.x, n.y + 1, n.cost + 1));
            }
        }
        
        while (q.Count > 0)
        {
            var n = q.Dequeue();
            if (n.cost < pathlen[x, z])
            {
                if (walkable[x, z])
                {
                    pathlen[x, z] = n.cost;
                    q.Enqueue(new Node(n.x - 1, n.y, n.cost + 1));
                    q.Enqueue(new Node(n.x + 1, n.y, n.cost + 1));
                    q.Enqueue(new Node(n.x, n.y - 1, n.cost + 1));
                    q.Enqueue(new Node(n.x, n.y + 1, n.cost + 1));
                }
            }
        }
    }

    public void destroy(int x, int z)
    {
        walkable[x, z] = true;
        pathlen[x, z] = Math.Min(
            pathlen[x+1, z],
            Math.Min(
                pathlen[x-1, z],
                Math.Min(
                    pathlen[x, z+1],
                    pathlen[x, z-1]
                )
            )
        );
        PriorityQueue<Node> q = new PriorityQueue<Node>();
        if (pathlen[x, z] < int.MaxValue)
        {
            pathlen[x, z] = pathlen[x, z] + 1;
            q.Enqueue(new Node(x + 1, z, pathlen[x, z] + 1));
            q.Enqueue(new Node(x - 1, z, pathlen[x, z] + 1));
            q.Enqueue(new Node(x, z - 1, pathlen[x, z] + 1));
            q.Enqueue(new Node(x, z + 1, pathlen[x, z] + 1));
        }

        
        while (q.Count > 0)
        {
            var n = q.Dequeue();
            if (n.cost < pathlen[x, z])
            {
                
                if (walkable[x, z])
                {
                    pathlen[x, z] = n.cost;
                    q.Enqueue(new Node(n.x - 1, n.y, n.cost + 1));
                    q.Enqueue(new Node(n.x + 1, n.y, n.cost + 1));
                    q.Enqueue(new Node(n.x, n.y - 1, n.cost + 1));
                    q.Enqueue(new Node(n.x, n.y + 1, n.cost + 1));
                }
            }
        }
    }
}
