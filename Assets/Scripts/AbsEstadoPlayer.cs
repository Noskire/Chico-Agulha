using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public abstract class AbsEstadoPlayer
{
    public string parametro;

    public void PararAninmacao(AbsPlayer player)
    {
       player.animator.SetBool(parametro, false);
    }
    public abstract void Andar(AbsPlayer player);
    public abstract void Morrer(AbsPlayer player);
    public abstract void Cair(AbsPlayer player);
    public abstract void TomarDano(AbsPlayer player);
    public abstract void Ataque(AbsPlayer player);
    public abstract void Bloqueio(AbsPlayer player);
    public abstract void Pular(AbsPlayer player);
    public abstract void Parado(AbsPlayer player);
    
}
