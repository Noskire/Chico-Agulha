using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour{
	public int damage, speed = 10;
	public bool isEnemyShot; //If true, hit player. If false, hit enemy.
	public bool toTheRight; //If true, move to the right. If false, move to the left.
	public float error;

	public GameObject player;
	public GameObject[] enemies;
	public Player playerScript;
	public Vector3 bound;


	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		playerScript = (Player)player.GetComponent("Player");
		bound = (transform.renderer.bounds.max - transform.renderer.bounds.min);

		if(toTheRight){
			transform.localPosition += new Vector3(0.5f, 0, 0);
		}else{
			transform.localPosition -= new Vector3(0.5f, 0, 0);
		}

		Destroy(gameObject, 10); //Avoid fly indefinitely and consume memory
	}

	void Update(){
		if(toTheRight){
			transform.localPosition += (new Vector3(speed, 0f, 0f) * Time.deltaTime);
		}else{
			transform.localPosition -= (new Vector3(speed, 0f, 0f) * Time.deltaTime);
		}
		if(isEnemyShot == true){ //Avoid friendly fire, hit only player
			if(transform.renderer.bounds.min.x + error < player.transform.renderer.bounds.max.x &&
			   transform.renderer.bounds.max.x - error > player.transform.renderer.bounds.min.x){ //Collide in x
				if(transform.renderer.bounds.min.y + error < player.transform.renderer.bounds.max.y &&
				   transform.renderer.bounds.max.y - error > player.transform.renderer.bounds.min.y){ //Collide in y
					playerScript.getHit(damage);
					Destroy(gameObject); //Destroy
				}
			}
		}
	}
}