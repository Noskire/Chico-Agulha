using UnityEngine;
using System.Collections;


public abstract class AbsPersonagem : MonoBehaviour {

    public float velocidade;
    public Animator animator;
    public Transform ground; //solo
    public int hp;
    public int hpMax;


    
    public abstract void Andar();
    public abstract void Cair();
    public abstract void Morrer();
    public abstract void TomarDano();
    public abstract void Ataque();
    public abstract void Bloqueio();
    public abstract void Parador();
    public abstract void Pular();

	// Use this for initialization
    public virtual void Start(){
        this.animator = this.gameObject.GetComponentInChildren<Animator>();
        this.ground = this.gameObject.GetComponentInChildren<Transform>();
    }
	
	// Update is called once per frame
    public virtual void Update() {}

}
