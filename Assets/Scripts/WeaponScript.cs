using UnityEngine;

public class WeaponScript : MonoBehaviour{
	public Transform shotPrefab;
	public float shootingRate = 0.25f;
	public bool toTheRight = true;
	private float shootCooldown;
	
	void Start(){
		shootCooldown = 0f;
	}
	
	void Update(){
		if (shootCooldown > 0){
			shootCooldown -= Time.deltaTime;
		}
	}
	
	public void Attack(bool isEnemy){
		if(CanAttack){
			shootCooldown = shootingRate;
			var shotTransform = Instantiate(shotPrefab) as Transform;
			shotTransform.position = transform.position;

			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
			if(shot != null){
				shot.isEnemyShot = isEnemy;
			}

			MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
			if(move != null){
				if(toTheRight){
					move.direction = this.transform.right;
				}else{
					move.direction = -this.transform.right;
				}
			}
		}
	}
	
	public bool CanAttack{
		get{
			return shootCooldown <= 0f;
		}
	}
}