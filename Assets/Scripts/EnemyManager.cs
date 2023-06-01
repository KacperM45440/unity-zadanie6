using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int _healthpoints;
    private Animator _animator;

    private void Awake()
    {
        _healthpoints = 10;
    }

    public void Flip()
    {
        _animator = transform.GetComponent<Animator>();
        _animator.SetBool("Flip", true);
    }

    public bool TakeHit()
    {
        _healthpoints -= 10;
        bool isDead = _healthpoints <= 0;
        if (isDead) _Die();
        return isDead;
    }

    private void _Die()
    {
        Destroy(gameObject);
    }
}