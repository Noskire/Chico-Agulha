using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EstPlayerBaladeira : AbsEstadoPlayer
{

    public override void Andar(AbsPlayer player)
    {
        this.parametro = "Attacking";
        player.animator.SetBool(parametro, true);
    }

    public override void Morrer(AbsPlayer player)
    {
        throw new NotImplementedException();
    }

    public override void Cair(AbsPlayer player)
    {
        throw new NotImplementedException();
    }

    public override void TomarDano(AbsPlayer player)
    {
        throw new NotImplementedException();
    }

    public override void Ataque(AbsPlayer player)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}
