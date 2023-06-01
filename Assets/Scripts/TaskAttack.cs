using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskAttack : Node
{
    private Transform _lastTarget;
    private Animator _animator;
    private EnemyManager _enemyManager;
    private float _attackTime = 1.5f;
    private float _attackCounter = 0f;

    public TaskAttack(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != null && target != _lastTarget)
        {
            _enemyManager = target.GetComponent<EnemyManager>();
            _lastTarget = target;
            _attackCounter = 0f;
        }

        if (_lastTarget == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        _attackCounter += Time.deltaTime;

        if (_attackCounter >= 0.40f)
        {
            _enemyManager.Flip();
        }

        if (_attackCounter >= _attackTime)
        {
            bool enemyIsDead = _enemyManager.TakeHit();
            if (enemyIsDead)
            {
                ClearData("target");
                _animator.SetBool("Attacking", false);
                _animator.SetBool("Walking", true);
                state = NodeState.SUCCESS;
            }
            else
            {
                _attackCounter = 0f;
                state = NodeState.RUNNING;
            }
        }


        state = NodeState.RUNNING;
        return state;
    }

}