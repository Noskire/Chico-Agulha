using UnityEngine;
using System.Collections;

public class ExperienceScript : MonoBehaviour {
	public int level = 1;
	public float exp = 0;
	private float maxExp = 100;

	void Update(){
		//gainExp(2);
	}

	public void gainExp(float e){
		exp += e;
		while(exp >= maxExp){
			level++;
			exp -= maxExp;
		}
	}
}
