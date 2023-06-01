using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckEnemies : Node
{
    private Transform _transform;
    private static int _enemyLayerMask = 1 << 6;
    private Animator _animator;
    public CheckEnemies(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t != null)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        Collider[] colliders = Physics.OverlapSphere(
        _transform.position, BossBT.fovRange, _enemyLayerMask);
        

        if(colliders.Length > 0)
        {
            parent.parent.SetData("target", colliders[0].transform);
            _animator.SetBool("Walking", true);
            state = NodeState.SUCCESS;
            return state;
        }

        else
        {
        state = NodeState.FAILURE;
        }
        return state;
    }
}
