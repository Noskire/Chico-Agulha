using UnityEngine;
using System.Collections;


public abstract class AbsPersonagem : MonoBehaviour {

    public float maxSpeed;
    public float maxImpulse;
    public float gravity;
    public Animator animator;
    public int eixoX;
    public int eixoY;
    public float impulso;
    public float velocidade;
    public float yInicial;
    public int hp;


    
    public abstract void Andar();
    public abstract void Cair();
    public abstract void Morrer();
    public abstract void TomarDano();
    public abstract void Ataque();
    public abstract void Bloqueio();
    public abstract void Parador();
    public abstract void Pular();

	// Use this for initialization
    public virtual void Start(){}
	
	// Update is called once per frame
    public virtual void Update() {}

}
