using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public int hp;
	public bool isEnemy;
	private Animator animator;

	void Start(){ //Use this for initialization
		animator = GetComponent<Animator>();
		//animator = this.transform.parent.GetComponent<Animator>();
	}

	public void Damage(int damageCount){
		hp -= damageCount;
		if(hp <= 0){ //Dead!
			animator.SetBool("Dead", true);
			Destroy(gameObject, 5);
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		//Is this a shot?
		ShotScript shot = collider.gameObject.GetComponent<ShotScript>();
		if(shot != null){
			//Avoid friendly fire
			if(shot.isEnemyShot != isEnemy){
				if(hp > 0){
					animator.SetBool("Hit", true);
				}
				Damage(shot.damage);
				Destroy(shot.gameObject); //Destroy the shot
			}
		}
	}
}
