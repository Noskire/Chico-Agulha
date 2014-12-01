using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour{
	//Finite State Machine
	private string[] states = new string[] {"Cutscene", "Playing", "Dead"}; //States of FSM
	private string state;

	//Player Attributes
	public int level, experience, skillPoints, gold, //Level, xp, points to distribute and money
		hp, speed, //HP and Movement speed
		meleeDamage, rangedDamage; //Damage with Needle and Estilingue
		
	private int maxLevel, maxExperience, maxSkillPoints, maxGold, maxHp, maxDamage, maxSpeed;

	//Entry
	private float inputX, inputY; //Movement in X and Y
	private bool jump, meleeAttack, rangedAttack, block; //Self explanatory

	//Auxiliary Variables
	private float moe; //Margin of error
	private float impulse, gravity, velocity, yInitial; //For Jump
	private bool isJumping; //True if jumping
	public Vector2 meleeRange; //Range of melee attack
	public Rect border; //Boundary of movement of player
	public bool debugMode; //Show things

	private Animator animator;
	public Transform shotPrefab; //Prefab of projectile

	void Start(){
		animator = GetComponent<Animator>();

		//Initializing variables
		state = states[1]; //Playing
		moe = 0.1f;
		impulse = 0.4f;
		gravity = 0.025f;
		isJumping = false;
		debugMode = true;

		//Player Status
		level = 1;
		experience = 0;
		skillPoints = 0;
		gold = 0;
		hp = 100;
		meleeDamage = 5;
		rangedDamage = 5;
		speed = 4;

		//Player Status Limit
		maxLevel = 100;
		maxExperience = 100;
		maxSkillPoints = 99;
		maxGold = 999999;
		maxHp = 100;
		maxDamage = 50;
		maxSpeed = 8;
	}

	void Update(){
		if(state == "Cutscene"){
			if(debugMode){
				print("Current State: Cutscene");
			}
		}else if(state == "Playing"){
			if(debugMode){
				Debug.DrawLine(new Vector3(border.xMin, border.yMin, 0f), new Vector3(border.xMax, border.yMin, 0f), Color.white);
				Debug.DrawLine(new Vector3(border.xMin, border.yMin, 0f), new Vector3(border.xMin, border.yMax, 0f), Color.white);
				Debug.DrawLine(new Vector3(border.xMax, border.yMax, 0f), new Vector3(border.xMax, border.yMin, 0f), Color.white);
				Debug.DrawLine(new Vector3(border.xMax, border.yMax, 0f), new Vector3(border.xMin, border.yMax, 0f), Color.white);
			}

			horizontalMovement(); //Moviment x
			jumpFunction(); //Jump
			if(!isJumping){ //While jumping, can only move horizontally
				verticalMovement(); //Moviment y
				meleeAttackFunction(); //Attack
				blockFunction(); //Block
				rangedAttackFunction(); //Shoot
			}
			//Disable Hurt and Dead Animation
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")){
				animator.SetBool("Hit", false);
			}
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("Dead")){
				//Chico doesn't revive, this is to not initiate the animation over and over again
				animator.SetBool("Dead", false);
				transform.parent.gameObject.AddComponent<GameOver>(); //Game Over
				state = "Dead";
			}
		}else if(state == "Dead"){
			if(debugMode){
				print("Current State: Dead");
			}
		}else{
			if(debugMode){
				print("Bug!");
			}
		}
	}

	public void horizontalMovement(){
		inputX = Input.GetAxis("Horizontal");
		if(inputX > moe){
			transform.localScale = new Vector3(1f, 1f, 1f);
			transform.localPosition += (new Vector3(speed, 0, 0) * Time.deltaTime);
			if(transform.localPosition.x > border.xMax){
				transform.localPosition = new Vector3(border.xMax, transform.localPosition.y, 0);
			}
		}else if(inputX < -moe){
			transform.localScale = new Vector3(-1f, 1f, 1f);
			transform.localPosition -= (new Vector3(speed, 0, 0) * Time.deltaTime);
			if(transform.localPosition.x < border.x){
				transform.localPosition = new Vector3(border.x, transform.localPosition.y, 0);
			}
		}
	}

	public void verticalMovement(){
		inputY = Input.GetAxis("Vertical");
		if(inputY > moe){
			transform.localPosition += (new Vector3(0, speed, 0) * Time.deltaTime);
			if(transform.localPosition.y > border.yMax){
				transform.localPosition = new Vector3(transform.localPosition.x, border.yMax, 0);
			}
		}else if(inputY < -moe){
			transform.localPosition -= (new Vector3(0, speed, 0) * Time.deltaTime);
			if(transform.localPosition.y < border.y){
				transform.localPosition = new Vector3(transform.localPosition.x, border.y, 0);
			}
		}
		
		if(Mathf.Abs(inputX) > moe || Mathf.Abs(inputY) > moe){
			animator.SetBool("Running", true);
		}else{
			animator.SetBool("Running", false);
		}
	}

	public void jumpFunction(){
		jump = Input.GetButtonDown("Jump");
		var pos = transform.position;
		if(jump && !isJumping){
			animator.SetBool("Jumping", true);
			isJumping = true;
			velocity = impulse;
			yInitial = pos.y;
		}
		if(isJumping){
			pos.y += velocity;
			velocity -= gravity;
			if(pos.y <= yInitial){
				isJumping = false;
				animator.SetBool("Jumping", false);
				pos.y = yInitial;
			}
		}
		transform.position = pos;
	}

	public void meleeAttackFunction(){
		meleeAttack = Input.GetButtonDown("Fire1");
		if(meleeAttack){
			if(!(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") ||
			     animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2"))){
				
				animator.SetBool("Attacking", true);
				float playerDirection = transform.localScale.x;
				Vector2 playerPos = new Vector2(transform.localPosition.x, transform.localPosition.y);
				GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
				foreach(GameObject enemy in enemies){
					if(enemy != null){
						Vector2 enemyPos = new Vector2(enemy.transform.localPosition.x, enemy.transform.localPosition.y);
						if(playerDirection == 1){ //Looking to the right
							if(enemyPos.x > playerPos.x &&
							   enemyPos.x <= playerPos.x + meleeRange.x){ //Between player and maximum range
								if(enemyPos.y <= playerPos.y + meleeRange.y &&
								   enemyPos.y >= playerPos.y - meleeRange.y){
									print("Hit enemy " + enemy.name);
									//HIT
									enemy.GetComponent<Enemy01>().getHit(meleeDamage);
								}
							}
						}else{ //playerDirection = -1
							if(enemyPos.x < playerPos.x &&
							   enemyPos.x >= playerPos.x - meleeRange.x){ //Between player and maximum range
								if(enemyPos.y <= playerPos.y + meleeRange.y &&
								   enemyPos.y >= playerPos.y - meleeRange.y){
									print("-Hit enemy " + enemy.name);
									//HIT
									enemy.GetComponent<Enemy01>().getHit(meleeDamage);								}
							}
						}
					}
				}
			}
		}else{
			animator.SetBool("Attacking", false);
		}
	}

	public void blockFunction(){
		block = Input.GetButton("Fire3");
		if(block){
			animator.SetBool("Defending", true);
		}else{
			animator.SetBool("Defending", false);
		}
	}

	public void rangedAttackFunction(){
		rangedAttack = Input.GetButtonDown("Fire2");
		if(rangedAttack){
			if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Ranged Attack")){
				animator.SetBool("Shooting", true);
				var shotTransform = Instantiate(shotPrefab) as Transform;
				shotTransform.position = transform.position;
				Shot shot = shotTransform.gameObject.GetComponent<Shot>();
				if(shot != null){
					shot.damage = rangedDamage;
					shot.isEnemyShot = false;
					float playerDirection = transform.localScale.x;
					if(playerDirection == 1){ //Looking to the right
						shot.toTheRight = true;
					}else{
						shot.toTheRight = false;
					}
				}
			}
		}else{
			animator.SetBool("Shooting", false);
		}
	}

	public void getHit(int damage){
		if(hp > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Defend")){
			animator.SetBool("Hit", true);
			hp -= damage;
			if(hp <= 0){ //Dead!
				hp = 0;
				animator.SetBool("Dead", true);
			}
		}
	}

	public void heal(int cure){
		hp += cure;
		if(hp > maxHp){
			hp = maxHp;
		}
	}

	public void addExp(int e){
		experience += e;
		while(experience >= maxExperience){
			experience -= maxExperience;
			maxExperience *= 2;
			if(level < maxLevel){
				level++;
				skillPoints++;
			}
		}
	}

	public void changeGold(int g){
		gold += g;
		if(gold > maxGold){
			gold = maxGold;
		}
	}
}
