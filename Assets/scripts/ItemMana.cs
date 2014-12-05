using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ItemMana:AbsItem
{
    public int mpImcremento;
    public override void ExecutarEfeito()
    {
        if (this.gameObejectColisor.tag == "Player")
        {
            if ((this.gameObejectColisor.GetComponentInChildren<Player>() as Player).mp < (this.gameObejectColisor.GetComponentInChildren<Player>() as Player).mpMax)
            {
                (this.gameObejectColisor.GetComponentInChildren<Player>() as Player).mp += this.mpImcremento;
            }
            Destroy(gameObject);
        }
    }

    public override void Start()
    {
        base.Start();
        this.tempoMax = 5;
    }


}
