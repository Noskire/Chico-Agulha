using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player:AbsPersonagem
{
    
    public int mp;
    public int mpMax;
    public bool jumping;
    public EnumEstado estadoPlayer;
    public bool isGrounded; // verifica se esta no solo
    public bool jumped; //verifica se esta pulando
    public float jumpTime; // usando para verificar se parou de pular
    public float force;	//forca do pulo
    public float jumpDelay; // tempo da animaçao
    

    public override void Andar()
    {
        this.isGrounded = Physics2D.Raycast(this.transform.position, this.ground.position, 1 << LayerMask.NameToLayer("Plataforma"));
        this.animator.SetFloat("run", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            this.transform.eulerAngles = new Vector2(0, 0);
            this.transform.Translate(Vector2.right * this.velocidade * Time.deltaTime);

        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            this.transform.eulerAngles = new Vector2(0, 180);
            this.transform.Translate((Vector2.right) * this.velocidade * Time.deltaTime);
        }


        if(Input.GetButtonDown("Jump") && this.isGrounded && !this.jumped){
            this.rigidbody2D.AddForce(this.transform.up * this.force);
            this.jumpTime = this.jumpDelay;
            this.animator.SetTrigger("jump");
            this.jumped = true;
        }

        this.jumpTime -= Time.deltaTime;

        if (this.jumpTime <= 0 && this.isGrounded && this.jumped)
        {
            this.animator.SetTrigger("ground");
            this.jumped = false;
        }

    }

    public override void Cair()
    {
        throw new System.NotImplementedException();
    }

    public override void Morrer()
    {
        throw new System.NotImplementedException();
    }

    public override void TomarDano()
    {
        throw new System.NotImplementedException();
    }

    public override void Ataque()
    {
        throw new System.NotImplementedException();
    }

    public override void Bloqueio()
    {
        throw new System.NotImplementedException();
    }

    public override void Parador()
    {
        throw new System.NotImplementedException();
    }

    public override void Pular()
    {
        throw new System.NotImplementedException();
    }



    public override void Update()
    {
        this.Andar();
    }


    public override void Start()
    {
        base.Start();
        this.jumping = false;
        this.jumpTime = 0.0f;
        this.jumpDelay = 0.8f;
        this.isGrounded = true;
        this.force = 200f;
        this.gameObject.tag = "Player";
        this.hpMax = 100;
        this.hp = 10;
        this.mpMax = 100;
        this.mp = 10;
    }

}
