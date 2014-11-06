using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player:AbsPlayer
{

    public override void Start()
    { //Use this for initialization
        this.animator = this.gameObject.GetComponentInChildren<Animator>();
        this.impulso = Time.deltaTime * maxSpeed;
        this.velocidade = 2;
        this.pulando = false;
        this.AterarEstado(EnumEstado.EstPlayerAgulhas);
    }

    public override void Update() 
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        //if (Input.GetButtonDown)
        {
            this.transform.eulerAngles = new Vector2(0, 0);
            this.Andar();

        }
        else
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                this.transform.eulerAngles = new Vector2(0, 180);
                this.Andar();
            }
            else
                this.estado.PararAninmacao(this);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (this.estadoPlayer == EnumEstado.EstPlayerAgulhas)
            {
                this.AterarEstado(EnumEstado.EstPlayerBaladeira);
            }
            else
            {
                this.AterarEstado(EnumEstado.EstPlayerAgulhas);
            }
        }

            bool inputJ = Input.GetButtonDown("Jump");
			var pos = this.transform.position;
            if (inputJ && !this.pulando)
            {
                this.animator.SetBool("Jumping", true);
                this.pulando = true;
                this.velocidade = this.impulso;
                this.yInicial = pos.y;
            }
            if (this.pulando)
            {
                pos.y += this.velocidade;
                this.velocidade -= this.gravity;
                if (pos.y <= this.yInicial)
                {
                    this.pulando = false;
                    this.animator.SetBool("Jumping", true);
                    pos.y = this.yInicial;
                }
            }
            this.transform.position = pos;
    }

}


