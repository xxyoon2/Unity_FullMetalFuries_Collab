using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationAsset;

public class TrissAttack : MonoBehaviour, IPlayerAttackable
{
    public GameObject[] Colliders;

    private PlayerInput _input;
    private Animator _animator;


    public bool IsAttacking { get; private set; }
    private int _attack1Type;
    private int _attackStack;


    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_input.Attack1)
        {
            OnAttack1();
        }
        else if (_input.Attack2)
        {
            OnAttack2();
        }
        else if(_input.SpecialAttack)
        {
            OnSpecialAttack();
        }
        else if(_input.Dodge)
        {
            OnDodge();
        }
    }
    public void OnAttack1()
    {
        _attack1Type = (_attack1Type + 1) % 2;
        _animator.SetTrigger(PlayerAnimation.Attack1_1[_attack1Type]);
    }

    public void OnAttack2()
    {

        throw new System.NotImplementedException();
    }

    public void OnDodge()
    {
        throw new System.NotImplementedException();
    }

    public void OnSpecialAttack()
    {
        throw new System.NotImplementedException();
    }


    public void OnCollider(int attackType)
    {
        Colliders[attackType].SetActive(true);
    }

    public void OffCollider(int attackType)
    {
        Colliders[attackType].SetActive(false);
    }
}
