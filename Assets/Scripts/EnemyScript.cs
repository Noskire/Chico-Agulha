using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour{
	private WeaponScript[] weapons;

	void Awake(){
		weapons = GetComponentsInChildren<WeaponScript>();
	}
	
	void Update(){
		//Auto-fire
		foreach(WeaponScript weapon in weapons){
			//Auto-fire
			if(weapon != null && weapon.CanAttack){
				weapon.Attack(true);
			}
		}
	}
}