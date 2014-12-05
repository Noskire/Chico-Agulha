using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class AbsItem : MonoBehaviour
{
    public GameObject gameObejectColisor;
    private float tempoVida;
    public float tempoMax;
    public AudioClip audio;

    public abstract void ExecutarEfeito();

    public void OnCollisionEnter2D(Collision2D colisor)
    {
        this.gameObejectColisor = colisor.gameObject;
        this.ExecutarEfeito();
    }


    // Use this for initialization
    public virtual void Start()
    {
        this.tempoVida = 0;
        this.tempoMax = 5f;
    }

    // Update is called once p er frame
    public virtual void Update() {

        this.tempoVida += Time.deltaTime;

        if (this.tempoVida >= this.tempoMax){
            Destroy(this.gameObject);
            this.tempoVida = 0;
        }

    }

    

}
