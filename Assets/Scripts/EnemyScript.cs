using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour{
	private Animator animator;
	private HealthScript health;
	private WeaponScript[] weapons;

	void Awake(){
		animator = GetComponent<Animator>();
		health = GetComponent<HealthScript>();
		weapons = GetComponentsInChildren<WeaponScript>();
	}
	
	void Update(){
		if(health.hp > 0){
			//Auto-fire
			foreach(WeaponScript weapon in weapons){
				//Auto-fire
				if(weapon != null && weapon.CanAttack){
					weapon.Attack(true);
				}
			}
		}
		//Disable Hurt and Dead Animation
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")){
			animator.SetBool("Hit", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Dead")){
			animator.SetBool("Dead", false);
		}
	}
}