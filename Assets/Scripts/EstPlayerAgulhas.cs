using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EstPlayerAgulhas : AbsEstadoPlayer
{

    public override void Andar(AbsPlayer player)
    {
        this.parametro = "Running";
        player.animator.SetBool(parametro, true);
        //player.animator.Play("Run",0,0.07f);
    }

    public override void Morrer(AbsPlayer player)
    {
        player.animator.SetBool("Dead", true);
    }

    public override void Cair(AbsPlayer player)
    {
        throw new NotImplementedException();
    }

    public override void TomarDano(AbsPlayer player)
    {
        player.animator.SetBool("Hit", true);
    }

    public override void Ataque(AbsPlayer player)
    {
        player.animator.SetBool("Attacking", true);
    }

    public override void Bloqueio(AbsPlayer player)
    {
        throw new NotImplementedException();
    }

    public override void Parado(AbsPlayer player)
    {
        player.animator.Play("Stance", 0, 0.01f);
    }

    public override void Pular(AbsPlayer player)
    {
        var pos = player.transform.position;
        if (/*player.inputJ &&*/ !player.pulando)
        {
            this.parametro = "Jumping";
            player.animator.SetBool(parametro, true);
            player.pulando = true;
            player.velocidade = player.impulso;
            player.yInicial = pos.y;
        }
        if (player.pulando)
        {
            pos.y += player.velocidade;
            player.velocidade -= player.gravity;
            if (pos.y <= player.yInicial)
            {
                player.pulando = false;
                this.parametro = "Jumping";
                player.animator.SetBool(parametro, true);
                pos.y = player.yInicial;
            }
        }
        player.transform.position = pos;
    }
}

