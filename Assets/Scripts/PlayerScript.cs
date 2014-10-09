using UnityEngine;

public class PlayerScript : MonoBehaviour{
	public Vector2 speed = new Vector2(25, 25);
	private Vector2 movement;
	
	void Update(){
		//Retrieve axis information
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		movement = new Vector2(speed.x * inputX, speed.y * inputY);

		bool shoot = Input.GetButtonDown("Fire1");
		if(shoot){
			WeaponScript weapon = GetComponent<WeaponScript>();
			if(weapon != null){
				//false because the player is not an enemy
				weapon.Attack(false);
			}
		}
		//Bounds
		var dist = (transform.position - Camera.main.transform.position).z;
		var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
		var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
		var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
		var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
			transform.position.z);
	}
	
	void FixedUpdate(){
		//Move the game object
		rigidbody2D.velocity = movement * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D collision){
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
	}
}