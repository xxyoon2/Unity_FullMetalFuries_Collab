using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
    public interface ISkillUsable
    {
        enum SkillType
        {
            Attack1,
            Attack2,
            Dodge,
            SpecialAttack,
            SkillTypeCoount
        }

        public void Use(GameObject player);
    }

    public abstract class Skill: ISkillUsable
    {
        public string Names;
        public string Info;

        public abstract void Use(GameObject player);
    }

    public class Attack1 : Skill
    {
        public override void Use(GameObject player)
        {
            Debug.Log("Wrong Use of Skill: Attack1");
        }
    }

    public class Attack2 : Skill
    {
        public override void Use(GameObject player)
        {
            Debug.Log("Wrong Use of Skill: Attack2");
        }
    }

    public class Dodge : Skill
    {
        public override void Use(GameObject player)
        {
            Debug.Log("Wrong Use of Skill: Dodge");
        }
    }

    public class SpecialAttack : Skill
    {
        public override void Use(GameObject player)
        {
            Debug.Log("Wrong Use of Skill: SpecialAttack");
        }
    }
    
}

