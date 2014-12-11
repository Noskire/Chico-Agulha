using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	public GameObject target;
	public Vector2 boundary; //Limits of the camera (Left, Right)
	public float targetX; //Current coordinate x of target
	
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
		targetX = target.transform.position.x;
		if(targetX < boundary.x){ //The camera can no longer go to the left
			transform.localPosition = new Vector3(boundary.x, 0, -10);
		}else if(targetX > boundary.y){ //The camera can no longer go to the right
			transform.localPosition = new Vector3(boundary.y, 0, -10);
		}else{ //Follow target
			transform.localPosition = new Vector3(targetX, 0, -10);
		}
	}
}
