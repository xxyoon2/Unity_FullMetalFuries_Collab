using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoplite_Movement : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Target;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Vector2.Distance(Target.transform.position, Enemy.transform.position) > 5f)
        {
            Move();
            // if (Vector2.Distance(Target.transform.position, Enemy.transform.position) < 1.5f)
            // {
            //     SetTrigger("Attack");
            // }
            
        }
    }

    void Move()
    {
        _animator.SetTrigger("Move");
        Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position, Target.transform.position, Time.deltaTime * 2f);
    }
}
