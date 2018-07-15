using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

public class PathFinding {
    static int size = 100;
    private int[,] pathlen = new int[size, size];
    private bool[,] walkable = new bool[size, size];
    private Tower[,] towers = new Tower[size, size];

    public void drawDebug()
    {
        var o = GameObject.Find("Player");


        for (var i = (int) Math.Max(0, o.transform.position.x-200); i < (int)Math.Min(size, o.transform.position.x - 200+10); i++)
        {
            for (var j = (int)Math.Max(0, o.transform.position.z - 200); j < (int)Math.Min(size, o.transform.position.z - 200+10); j++)
            {
                Handles.Label(new Vector3(200+i, 1, 200+j), ":"+pathlen[i, j]);
            }
        }
    }

    public bool canWalk(int x, int z)
    {
        return walkable[x, z];
    }

    public Tower getTower(int x, int z)
    {
        return towers[x, z];
    }

    public void runFirst(int x, int z)
    {
        for(var i=0; i<size;i++)
        {
            for (var j = 0; j< size; j++)
            {
                walkable[i, j] = true;
                pathlen[i, j] = int.MaxValue;
            }
        }


        pathlen[x, z] = 0;
        PriorityQueue< Node> q = new PriorityQueue<Node>();

        q.Enqueue(new Node(x - 1, z, 1));
        q.Enqueue(new Node(x + 1, z, 1));
        q.Enqueue(new Node(x, z - 1, 1));
        q.Enqueue(new Node(x, z + 1, 1));
        Debug.Log(q.Count);
        while (q.Count > 0)
        {
            var n = q.Dequeue();

            if (n.x >= 0 && n.x < size && n.y >= 0 && n.y < size)
            {


                if (n.cost < pathlen[n.x, n.y])
                {

                    if (walkable[n.x, n.y])
                    {
                        pathlen[n.x, n.y] = n.cost;
                        q.Enqueue(new Node(n.x - 1, n.y, n.cost + 1));
                        q.Enqueue(new Node(n.x + 1, n.y, n.cost + 1));
                        q.Enqueue(new Node(n.x, n.y - 1, n.cost + 1));
                        q.Enqueue(new Node(n.x, n.y + 1, n.cost + 1));
                    }
                }
            }
        }
    }

    public int getPath(int x, int z)
    {
        //Debug.Log("X:" + x + " , Z: " + z);
        if (pathlen[x + 1, z] < pathlen[x, z])
        {
            return 0;
        }
        if (pathlen[x - 1, z] < pathlen[x, z])
        {
            return 2;
        }
        if (pathlen[x, z+1] < pathlen[x, z])
        {
            return 1;
        }
        if (pathlen[x, z-1] < pathlen[x, z])
        {
            return 3;
        }
        return -1;
    }

    public void place(int x, int z, Tower tower)
    {
        walkable[x, z] = false;
        towers[x, z] = tower;
        
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
            if (n.x >= 0 && n.x < size && n.y >= 0 && n.y < size)
            {
                if (pathlen[n.x, n.y] < n.cost)
                {
                    q.Enqueue(new Node(n.x + 1, n.y, pathlen[n.x, n.y] + 1));
                    q.Enqueue(new Node(n.x - 1, n.y, pathlen[n.x, n.y] + 1));
                    q.Enqueue(new Node(n.x, n.y + 1, pathlen[n.x, n.y] + 1));
                    q.Enqueue(new Node(n.x, n.y - 1, pathlen[n.x, n.y] + 1));
                }
                else if (pathlen[n.x, n.y] != int.MaxValue)
                {
                    toremove.Enqueue(new Node(n.x + 1, n.y, n.cost + 1));
                    toremove.Enqueue(new Node(n.x - 1, n.y, n.cost + 1));
                    toremove.Enqueue(new Node(n.x, n.y - 1, n.cost + 1));
                    toremove.Enqueue(new Node(n.x, n.y + 1, n.cost + 1));
                    pathlen[n.x, n.y] = int.MaxValue;
                }
            }
        }

        while (q.Count > 0)
        {
            var n = q.Dequeue();

            if (n.x >= 0 && n.x < size && n.y >= 0 && n.y < size)
            {


                if (n.cost < pathlen[n.x, n.y])
                {

                    if (walkable[n.x, n.y])
                    {
                        pathlen[n.x, n.y] = n.cost;
                        q.Enqueue(new Node(n.x - 1, n.y, n.cost + 1));
                        q.Enqueue(new Node(n.x + 1, n.y, n.cost + 1));
                        q.Enqueue(new Node(n.x, n.y - 1, n.cost + 1));
                        q.Enqueue(new Node(n.x, n.y + 1, n.cost + 1));
                    }
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

            if (n.x >= 0 && n.x < size && n.y >= 0 && n.y < size)
            {


                if (n.cost < pathlen[n.x, n.y])
                {

                    if (walkable[n.x, n.y])
                    {
                        pathlen[n.x, n.y] = n.cost;
                        q.Enqueue(new Node(n.x - 1, n.y, n.cost + 1));
                        q.Enqueue(new Node(n.x + 1, n.y, n.cost + 1));
                        q.Enqueue(new Node(n.x, n.y - 1, n.cost + 1));
                        q.Enqueue(new Node(n.x, n.y + 1, n.cost + 1));
                    }
                }
            }
        }
    }
}
