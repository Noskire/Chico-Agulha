using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour{
	public Vector2 speed = new Vector2(25, 25);
	public float impulse;
	public float gravity;

	public float attackTimer;
	public float cooldown;
	//public Transform target;

	public float MeleeDamage = 10;
	public float LongRangeDamage = 5;

	private bool jumping = false;
	private float velocity, yInitial;
	private Vector2 movement;

	private Animator animator;
	private HealthScript health;
	private ExperienceScript experience;
	private WeaponScript weapon;

	void Start(){
		animator = GetComponent<Animator>();
		weapon = GetComponent<WeaponScript>();
		health = GetComponent<HealthScript>();
		experience = GetComponent<ExperienceScript>();
	}

	void OnGUI(){
		//Background box
		GUI.Box(new Rect(10,10,100,115), "Chico Agulha");
		//HP
		GUI.Label(new Rect(20,40,80,20), "HP: ");
		GUI.Label(new Rect(75,40,80,20), health.hp.ToString());
		//Level
		GUI.Label(new Rect(20,70,80,20), "Level: ");
		GUI.Label(new Rect(75,70,80,20), experience.level.ToString());
		//Experience
		GUI.Label(new Rect(20,100,80,20), "Exp.: ");
		GUI.Label(new Rect(75,100,80,20), experience.exp.ToString());
	}

	void Update(){
		if(health.hp > 0){
			//Moviment
			float inputX = Input.GetAxis("Horizontal");
			float inputY = Input.GetAxis("Vertical");
			movement = new Vector2(speed.x * inputX, speed.y * inputY);
			if(Mathf.Abs(movement.x) > 0.01 || Mathf.Abs(movement.y) > 0.01){ //0.01 = Error margin
				animator.SetBool("Running", true);
				if(movement.x > 0.01){
					transform.localScale = new Vector3(1f, 1f, 1f);
					weapon.toTheRight = true;
				}else if(movement.x < -0.01){
					transform.localScale = new Vector3(-1f, 1f, 1f);
					weapon.toTheRight = false;
				}
			}else{
				animator.SetBool("Running", false);
			}
			//Jump
			bool inputJ = Input.GetButtonDown("Jump");
			var pos = transform.position;
			if(inputJ && !jumping){
				animator.SetBool("Jumping", true);
				jumping = true;
				velocity = impulse;
				yInitial = pos.y;
			}
			if(jumping){
				pos.y += velocity;
				velocity -= gravity;
				if(pos.y <= yInitial){
					jumping = false;
					animator.SetBool("Jumping", false);
					pos.y = yInitial;
				}
			}
			transform.position = pos;
			//Attack
			bool meleeAttack = Input.GetButtonDown("Fire1");
			if(attackTimer > 0){
				attackTimer--;
			}
			if(meleeAttack){
				if(attackTimer <= 0){
					attackTimer = cooldown;
					animator.SetBool("Attacking", true);
					Attack();
				}
			}else{
				animator.SetBool("Attacking", false);
			}
			//Block
			bool inputB = Input.GetButton("Fire2");
			if(inputB){
				animator.SetBool("Defending", true);
			}else{
				animator.SetBool("Defending", false);
			}
			//Shoot
			bool shoot = Input.GetButtonDown("Fire3");
			if(shoot){
				//animator.SetBool("Attacking", true);
				WeaponScript weapon = GetComponentInChildren<WeaponScript>();
				if(weapon != null && weapon.CanAttack){
					//false because the player is not an enemy
					weapon.Attack(false, LongRangeDamage);
				}
			}else{
				//animator.SetBool("Attacking", false);
			}
			//Bounds
			/*var dist = (transform.position - Camera.main.transform.position).z;
			var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
			var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
			var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
			var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
			transform.position = new Vector3(
				Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
				Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
				transform.position.z);*/
		}else{
			movement = new Vector2(0, 0);
		}
		//Disable Hurt and Dead Animation
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")){
			animator.SetBool("Hit", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Dead")){
			animator.SetBool("Dead", false);
		}
		EnforceBounds();
	}
	
	void FixedUpdate(){
		//Move the game object
		rigidbody2D.velocity = movement * Time.deltaTime;
	}

	private void Attack(){
		Transform enemy = null, target = null;
		bool targetBehind;
		float distance = 1000000, distEnemy;
		//Get the closest enemy
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject go in enemies){
			if(go != null){
				enemy = go.transform;
				distEnemy = Vector3.Distance(enemy.transform.position, transform.position);
				if(target == null){
					target = enemy;
					distance = distEnemy;
				}else{
					if(distEnemy < distance){
						target = enemy;
						distance = distEnemy;
					}
				}
			}
		}
		//Verify if it is behind
		if(transform.position.x < target.transform.position.x && transform.localScale.x == 1 ||
		   transform.position.x > target.transform.position.x && transform.localScale.x == -1){
			targetBehind = false; //Chico is facing the target
		}else{
			targetBehind = true;
		}
		//If close enough and is not behind, attack (Decreases target HP)
		if(distance <= 1.4 && !targetBehind){
			HealthScript eh = (HealthScript)target.GetComponent("HealthScript");
			eh.Damage(MeleeDamage);
		}
	}
	
	private void EnforceBounds(){
		Vector3 newPosition = transform.position;
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;
		
		float xDist = mainCamera.aspect * mainCamera.orthographicSize - 0.5f;
		float xMax = cameraPosition.x + xDist;
		float xMin = cameraPosition.x - xDist;

		if(newPosition.x < xMin || newPosition.x > xMax){
			newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
			movement.x = -movement.x;
		}
		//TODO vertical bounds

		transform.position = newPosition;
	}

	public void addExp(float exp){
		experience.gainExp(exp);
	}

	/*void OnCollisionEnter2D(Collision2D collision){
		bool damagePlayer = false;
		// Collision with enemy
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
		if(enemy != null){
			//Kill the enemy
			HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
			if(enemyHealth != null){
				enemyHealth.Damage(enemyHealth.hp);
			}
			damagePlayer = true;
		}
		// Damage the player
		if(damagePlayer){
			HealthScript playerHealth = this.GetComponent<HealthScript>();
			if(playerHealth != null){
				playerHealth.Damage(1);
			}
		}
	}*/
}