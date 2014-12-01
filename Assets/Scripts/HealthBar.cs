using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour{
	private Rect aux;
	private Player player;
	private GUITexture healthBar;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		healthBar = GetComponent<GUITexture>();
		aux = healthBar.pixelInset;
	}
	
	void Update(){
		healthBar.pixelInset = new Rect(aux.x, aux.y, player.hp * 2, aux.height);
	}
}
