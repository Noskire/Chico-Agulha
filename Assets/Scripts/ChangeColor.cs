using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour{
	public Color cor;
	// Use this for initialization
	void Start(){

	}
	
	// Update is called once per frame
	void Update(){
		renderer.material.color = cor;
	}
}