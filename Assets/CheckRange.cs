using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckRange : Node
{
    private Transform _transform;
    private Animator _animator;

    public CheckRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(_transform.position, target.position) <= BossBT.attackRange)
        {
            _animator.SetBool("Walking", false);
            _animator.SetBool("Attacking", true);


            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }

}