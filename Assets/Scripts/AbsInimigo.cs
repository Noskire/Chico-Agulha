using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class AbsInimigo:AbsPersonagem
{
    

    public void SortearAtaque()
    {
        Random ram = new Random(100);
        int at = ram.Next();
        if (at % 2 == 0)
        {
            this.Ataque(); 
        }
        else
            if (at % 3 == 0)
            {
                this.Ataque2();
            }
            else
            {
                this.Ataque3();
            }
    }




    public abstract void Ataque2();
    public abstract void Ataque3();


    public override void Andar()
    {
        throw new NotImplementedException();
    }

    public override void Cair()
    {
        throw new NotImplementedException();
    }

    public override void Morrer()
    {
        throw new NotImplementedException();
    }

    public override void TomarDano()
    {
        throw new NotImplementedException();
    }

    public override void Ataque()
    {
        throw new NotImplementedException();
    }

    public override void Bloqueio()
    {
        throw new NotImplementedException();
    }
}

