using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ItemLife:AbsItem
{
    public int hpIncremente;
    public override void ExecutarEfeito()
    {
        if (this.gameObejectColisor.tag == "Player")
        {
            if ((this.gameObejectColisor.GetComponentInChildren<Player>() as Player).hp < (this.gameObejectColisor.GetComponentInChildren<Player>() as Player).hpMax)
            {
                (this.gameObejectColisor.GetComponentInChildren<Player>() as Player).hp += this.hpIncremente;
                
            }
            Destroy(gameObject);
        }
    }

    public override void Start()
    {
        base.Start();
        this.tempoMax = 5;
        this.hpIncremente = 10;
    }


}
