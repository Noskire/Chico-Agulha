using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour{
	public Vector2 speed = new Vector2(25, 25);
	public float impulse;
	public float gravity;
	private bool jumping = false;
	private float velocity, yInitial;
	private Vector2 movement;
	private Animator animator;
	private HealthScript health;
	private WeaponScript weapon;

	void Start(){
		animator = GetComponent<Animator>();
		weapon = GetComponent<WeaponScript>();
		health = GetComponent<HealthScript>();
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
			bool shoot = Input.GetButtonDown("Fire1");
			if(shoot){
				animator.SetBool("Attacking", true);
				WeaponScript weapon = GetComponentInChildren<WeaponScript>();
				if(weapon != null && weapon.CanAttack){
					//false because the player is not an enemy
					weapon.Attack(false);
				}
			}else{
				animator.SetBool("Attacking", false);
			}
			//Block
			bool inputB = Input.GetButton("Fire3");
			if(inputB){
				animator.SetBool("Defending", true);
			}else{
				animator.SetBool("Defending", false);
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