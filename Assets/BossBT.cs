using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class BossBT : BehaviorTree.Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;
    public static float waitTime = 3f;
    public static float fovRange = 3f;
    public static float attackRange = 1f;
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckRange(transform),
                new TaskAttack(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemies(transform),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, waypoints),
        });

        return root;
    }
}
