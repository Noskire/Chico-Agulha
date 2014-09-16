using UnityEngine;
using System.Collections;

public class MMScript : MonoBehaviour {
	public float maxSpeed;
	public float maxImpulse;
	public Animator animator;
	//public GameObject projetile;
	bool pulando = false;
	
	void Start(){ //Use this for initialization
		animator = GetComponent<Animator>();
	}
	
	void Update(){ //Update is called once per frame
		//Instantiate(projetile);

		//Movimento
		if(Input.GetKey(KeyCode.A)){
			transform.localScale = new Vector3(-1f, 1f, 1f);
			rigidbody2D.velocity = new Vector2(-maxSpeed, rigidbody2D.velocity.y);
		}
		if(Input.GetKey(KeyCode.D)){
			rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
		}
		animator.SetFloat("Speed", rigidbody2D.velocity.x);

		//Pulo
		if(Input.GetKeyDown(KeyCode.Space)){
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, maxImpulse);
		}
		animator.SetFloat("Impulse", rigidbody2D.velocity.y);
	}
	
	void FixedUpdate(){ //Update com alta prioridade, como colisao
		
	}
}
