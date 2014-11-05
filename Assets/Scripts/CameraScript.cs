using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour{
	public GameObject target = null;
	public Vector3 positionOffset = Vector3.zero;

	void Start(){
		positionOffset = transform.position - target.transform.position;
	}
	
	void Update(){
		Vector3 newPosition;
		if(target != null){
			newPosition = target.transform.position + positionOffset;
			newPosition.y = 0;
			transform.position = newPosition;
		}
	}
}
