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
        Transform t = (Transform)GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        if (Vector3.Distance(_transform.position, t.position) <= BossBT.attackRange)
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