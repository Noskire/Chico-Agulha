using UnityEngine;
using System.Collections;

public class ChicoScript : MonoBehaviour {
	public float maxSpeed;
	public float maxImpulse;
	public float gravidade;
	public Animator animator;
	int eixoX, eixoY;
	float impulso, velocidade, yInicial;
	bool pulando;
	//public GameObject Chico;
	//public GameObject projetile;

	void Start(){ //Use this for initialization
		animator = GetComponent<Animator>();
		impulso = Time.deltaTime * maxSpeed;
		velocidade = 0;
		pulando = false;
		//Chico = GetComponent<GameObject>();
	}

	void Update(){ //Update is called once per frame
		//Instantiate(projetile);
		eixoX = 0;
		eixoY = 0;
		//Movimento Vertical
		if(Input.GetKey(KeyCode.W)){
			eixoY = 1;
		}
		if(Input.GetKey(KeyCode.S)){
			eixoY = -1;
		}
		//Movimento Horizontal
		if(Input.GetKey(KeyCode.A)){
			transform.localScale = new Vector3(-1f, 1f, 1f);
			eixoX = -1;
		}
		if(Input.GetKey(KeyCode.D)){
			transform.localScale = new Vector3(1f, 1f, 1f);	
			eixoX = 1;
		}
		transform.Translate(impulso * eixoX, impulso * eixoY, 0);
		float f = 4.5f;
		var pos = transform.position;
		//Limitadores
		/*if(pos.x < -f){
			pos.x = -f;
		}
		if(pos.x > f){
			pos.x = f;
		}
		if(pos.y < -0.6f){
			pos.y = -0.6f;
		}
		if(pos.y > 1.5f){
			pos.y = 1.5f;
		}*/
		if(eixoX != 0 || eixoY != 0){
			animator.SetFloat ("Speed", impulso);
		}else{
			animator.SetFloat ("Speed", 0);
		}
		//Pulo
		if(Input.GetKeyDown(KeyCode.Space) && !pulando){
			pulando = true;
			velocidade = maxImpulse;
			yInicial = pos.y;
		}
		if(pulando){
			pos.y += velocidade;
			velocidade += gravidade;
			if(pos.y <= yInicial){
				pulando = false;
				pos.y = yInicial;
			}
		}
		transform.position = pos;
		//Defesa
		if(Input.GetKey(KeyCode.J)){
			animator.SetBool("Block", true);
		}
		if(Input.GetKeyUp(KeyCode.J)){
			animator.SetBool("Block", false);
		}
		//Ataque
		if(Input.GetKeyDown(KeyCode.K)){
			animator.SetBool("Attack", true);
		}
		if(Input.GetKeyUp(KeyCode.K)){
			animator.SetBool("Attack", false);
		}
	}
	
	void FixedUpdate(){ //Update com alta prioridade, como colisao
		
	}
}
