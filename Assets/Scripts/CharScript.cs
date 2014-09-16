using UnityEngine;
using System.Collections;

public class CharScript : MonoBehaviour{
	public float maxSpeed;
	public Animator animator;
	bool pulando = false;

	void Start(){ //Use this for initialization
		animator = GetComponent<Animator>();
	}

	void Update(){ //Update is called once per frame
		if(Input.GetKeyDown(KeyCode.Space)){
			transform.position = new Vector2(transform.position.x, (transform.position.y + 2.5f));
			//rigidbody2D.AddForce(new Vector2(0.0f, 400.0f));
			//pulando = true;
		}
		if(Input.GetKey(KeyCode.A)){
			transform.position = new Vector2((transform.position.x - 0.2f), transform.position.y);
		}
		if(Input.GetKey(KeyCode.D)){
			//transform.position = new Vector2((transform.position.x + 0.25f), transform.position.y);
			rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
		}
		animator.SetFloat("Speed", maxSpeed);
	}

	void FixedUpdate(){ //Update com alta prioridade, como colisao

	}
}
