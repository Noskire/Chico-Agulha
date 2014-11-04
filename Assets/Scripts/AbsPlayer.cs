using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class AbsPlayer : AbsCharacter
{
    protected int mp;
    protected bool jumping;
    protected int xp;
    protected int lvl;

    public abstract void Start();
    public abstract void Update();

    public override void Walk()
    {
        this.transform.Translate(Vector2.right * this.velocityX * this.impulse);
        this.animator.SetBool("Running", true);
    }

    public override void Fall()
    {
        throw new NotImplementedException();
    }

    public override void Hit()
    {
        throw new NotImplementedException();
    }

    public override void Die()
    {
        throw new NotImplementedException();
    }

    public override void Attack()
    {
        animator.SetBool("Attacking", true);
    }

    public override void Attack2()
    {

        animator.SetBool("Attacking", true);
        WeaponScript weapon = GetComponentInChildren<WeaponScript>();
        if (weapon != null && weapon.CanAttack)
        {
            weapon.Attack(false);
        }
    }

    public override void Block()
    {
        animator.SetBool("Defending", true);
    }
}

