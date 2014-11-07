using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour{
	public Transform target;
	public float attackTimer;
	public float cooldown;
	public float speed;
	public float maxDist;
	public float experience = 10;
	public float MeleeDamage = 10;
	public float LongRangeDamage = 2.5f;
	public Vector3 movement = new Vector3(0f, 0f, 0f);

	private Animator animator;
	private HealthScript health;
	private WeaponScript[] weapons;
	private WeaponScript weapon;
	private Transform myTransform;

	void Awake(){
		animator = GetComponent<Animator>();
		health = GetComponent<HealthScript>();
		weapons = GetComponentsInChildren<WeaponScript>();
		weapon = GetComponent<WeaponScript>();
		myTransform = transform;
	}

	void Start(){
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
	}

	void Update(){
		float dist = Vector3.Distance (target.position, myTransform.position);
		if(dist <= maxDist){
			//Debug.DrawLine(target.position, myTransform.position, Color.blue);
			//Find quadrant of target
			//x
			if(target.position.x < myTransform.position.x - 1){
				movement.x = -1;
			}else if(target.position.x > myTransform.position.x + 1){
				movement.x = 1;
			}else{
				movement.x = 0;
			}
			//y
			if(target.position.y < myTransform.position.y - 1){
				movement.y = -1;
			}else if(target.position.y > myTransform.position.y + 1){
				movement.y = 1;
			}else{
				movement.y = 0;
			}

			if(health.hp > 0){
				//Look at target
				if(movement.x == 1){
					transform.localScale = new Vector3(1f, 1f, 1f);
					weapon.toTheRight = true;
				}else if(movement.x == -1){
					transform.localScale = new Vector3(-1f, 1f, 1f);
					weapon.toTheRight = false;
				}
				//Move towards target
				myTransform.position += movement * speed * Time.deltaTime;
				//Melee Attack
				if(attackTimer > 0){
					attackTimer--;
				}
				/*bool playerBehind;
				if(myTransform.position.x < target.position.x && myTransform.localScale.x == 1 ||
					myTransform.position.x > target.position.x && myTransform.localScale.x == -1){
					playerBehind = false; //Enemy is facing Chico
				}else{
					playerBehind = true;
				}*/
				float distance = Vector3.Distance(target.position, myTransform.position);
				//Is close and can attack
				if(distance <= 1.4 && attackTimer <= 0){
					attackTimer = cooldown;
					animator.SetBool("Attacking", true);
					HealthScript eh = (HealthScript)target.GetComponent("HealthScript");
					eh.Damage(MeleeDamage);
				}else{
					animator.SetBool("Attacking", false);
				}
			}
		}else{ //dist > maxDist
			movement.x = 0;
			//y
			if(target.position.y < myTransform.position.y - 0.1){
				movement.y = -1;
			}else if(target.position.y > myTransform.position.y + 0.1){
				movement.y = 1;
			}else{
				movement.y = 0;
			}
			//Move towards target
			myTransform.position += movement * speed * Time.deltaTime;

			if(health.hp > 0){
				//Auto-fire
				foreach(WeaponScript weapon in weapons){
					//Auto-fire
					if(weapon != null && weapon.CanAttack){
						weapon.Attack(true, LongRangeDamage);
					}
				}
			}
			//Disable Hurt and Dead Animation
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")){
				animator.SetBool("Hit", false);
				attackTimer = cooldown; //Can't attack while being hit
			}
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("Dead")){
				animator.SetBool("Dead", false);
			}
		}
	}
}