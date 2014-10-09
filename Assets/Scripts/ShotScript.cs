using UnityEngine;

public class ShotScript : MonoBehaviour{
	public int damage = 1;
	public bool isEnemyShot = false;
	
	void Start(){
		//Avoid fly indefinitely and consume memory
		Destroy(gameObject, 10);
	}
}