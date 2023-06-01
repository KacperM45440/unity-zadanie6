using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Transform[] _waypoints;
    private Animator _animator;
    private int _currentWaypointIndex = 0;

    public float _waitTime = BossBT.waitTime; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _waiting = false;
                _animator.SetBool("Gangnam", false);
                _animator.SetBool("Walking", true);
            }
        }
        else
        {
            Transform wp = _waypoints[_currentWaypointIndex];
            if (Vector3.Distance(_transform.position, wp.position) < 0.01f)
            {
                _transform.position = wp.position;
                _waitCounter = 0f;
                _waiting = true;


                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                _animator.SetBool("Walking", false);
                _animator.SetBool("Gangnam", true);


            }
            else
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position,
                    wp.position,
                    BossBT.speed * Time.deltaTime);
                _transform.LookAt(wp.position);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }

}
