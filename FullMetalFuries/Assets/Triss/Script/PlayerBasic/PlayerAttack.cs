using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerAttackable
{
    public enum AttackType
    {
        Attack1,
        Attack2,
        Dodge,
        SpecialAttack
    }

    void OnAttack1();
    void OnAttack2();
    void OnSpecialAttack();
    void OnDodge();

    bool IsAttacking();
}
