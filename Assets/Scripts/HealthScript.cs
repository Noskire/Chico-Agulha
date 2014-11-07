using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour{
	public float hp;
	private float maxHP;
	public bool isEnemy;
	private Animator animator;

	void Start(){ //Use this for initialization
		maxHP = hp;
		animator = GetComponent<Animator>();
		//animator = this.transform.parent.GetComponent<Animator>();
	}

	void Update(){
		if(hp < 0){
			hp = 0;
		}else if(hp > maxHP){
			hp = maxHP;
		}
	}

	public void Damage(float damageCount){
		if(hp > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Defend")){
			animator.SetBool("Hit", true);
			hp -= damageCount;
			if(hp <= 0){ //Dead!
				animator.SetBool("Dead", true);
				gameObject.rigidbody2D.isKinematic = true;
				Destroy(gameObject, 5);
				//Add exp of enemy to player
				GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().addExp(gameObject.GetComponent<EnemyScript>().experience);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		//Is this a shot?
		ShotScript shot = collider.gameObject.GetComponent<ShotScript>();
		if(shot != null){
			//Avoid friendly fire
			if(shot.isEnemyShot != isEnemy){
				//if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Defend")){ //Not blocking
					/*if(hp > 0){
						animator.SetBool("Hit", true);
					}*/
					Damage(shot.damage);
				//}
				Destroy(shot.gameObject); //Destroy the shot
			}
		}
	}
}
