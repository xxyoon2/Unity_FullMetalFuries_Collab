using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationAsset
{
    public static class PlayerAnimation
    {
        public static readonly int Idel = Animator.StringToHash("Idel");
        public static readonly int Idel2 = Animator.StringToHash("Idel2");
        public static readonly int Move = Animator.StringToHash("Move");

        public static readonly int Attack1_1_1 = Animator.StringToHash("Attack1_1(1)");
        public static readonly int Attack1_1_2 = Animator.StringToHash("Attack1_1(2)");
        public static readonly int[] Attack1_1 = new int[]
        {
            Attack1_1_1, Attack1_1_2
        };
        public static readonly int Attack1_2 = Animator.StringToHash("Attack1_2");
        public static readonly int Attack1_3 = Animator.StringToHash("Attack1_3");

        public static readonly int Attack2 = Animator.StringToHash("Attack2");
        // 트리스 전용
        public static readonly int Shield = Attack2;
    }
}