using UnityEngine;
using System.Collections;
using Assets.Scripts;
//=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AbsPlayer : AbsPersonagem {

    public int mp;
    public bool pulando;
    public EnumEstado estadoPlayer;
    public AbsEstadoPlayer estado;



    public void AterarEstado(EnumEstado estado) {
        switch (estado)
        {
            case EnumEstado.EstPlayerAgulhas: 
                    this.estado = new EstPlayerAgulhas();  
                    break;
            case EnumEstado.EstPlayerBaladeira: 
                    this.estado = new EstPlayerBaladeira(); 
                    break;
            default: 
                    throw new PlayerEstadoException("Estado não existe!");
        }

        this.estadoPlayer = estado;

    }


    public override void Andar()
    {
        this.estado.Andar(this);
        this.transform.Translate(Vector2.right * this.velocidade * Time.deltaTime);
        
    }

    public override void Cair()
    {
        this.estado.Cair(this);
    }

    public override void Morrer()
    {
        this.estado.Morrer(this);
    }

    public override void TomarDano()
    {
        this.estado.TomarDano(this);
    }

    public override void Ataque()
    {
        this.estado.Ataque(this);
    }

    public override void Bloqueio()
    {
        this.estado.Bloqueio(this);
    }

    public override void Parador()
    {
        this.estado.Parado(this);
    }

    public override void Pular()
    {
        this.estado.Pular(this);
    }
}

/*public abstract class AbsPlayer : AbsCharacter
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
}*/

//>>>>>>> 9bfbedd14206e04b0076736106c6909f5bad60b6

