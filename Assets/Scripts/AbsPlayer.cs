using UnityEngine;
using System.Collections;
using Assets.Scripts;

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
