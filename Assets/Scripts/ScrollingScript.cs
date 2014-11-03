﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollingScript : MonoBehaviour{
	public Vector2 speed = new Vector2(2, 2);
	public Vector2 direction = new Vector2(-1, 0);
	public bool isLinkedToCamera = false;
	public bool isLooping = false;
	private List<Transform> backgroundPart;

	void Start(){
		if(isLooping){
			// Get all the children of the layer with a renderer
			backgroundPart = new List<Transform>();
			for(int i = 0; i < transform.childCount; i++){
				Transform child = transform.GetChild(i);
				//Add only the visible children
				if(child.renderer != null){
					backgroundPart.Add(child);
				}
			}
			//Sort by position.
			backgroundPart = backgroundPart.OrderBy(t => t.position.x).ToList();
		}
	}
	
	void Update(){
		/*Vector3 movement = new Vector3(
			speed.x * direction.x,
			speed.y * direction.y,
			0);
		movement *= Time.deltaTime;
		transform.Translate(movement);
		// Move the camera
		if(isLinkedToCamera){
			Camera.main.transform.Translate(movement);
		}*/

		if(isLooping){
			//Get the first object.
			Transform firstChild = backgroundPart.FirstOrDefault();
			if(firstChild != null){
				if(firstChild.position.x < Camera.main.transform.position.x){
					if(firstChild.renderer.IsVisibleFrom(Camera.main) == false){
						//Get the last child position.
						Transform lastChild = backgroundPart.LastOrDefault();
						Vector3 lastPosition = lastChild.transform.position;
						Vector3 lastSize = (lastChild.renderer.bounds.max - lastChild.renderer.bounds.min);
						firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);
						backgroundPart.Remove(firstChild);
						backgroundPart.Add(firstChild);
					}
				}
			}
			//Get the last object.
			Transform finalChild = backgroundPart.LastOrDefault();
			if(finalChild != null){
				if(finalChild.position.x > Camera.main.transform.position.x){
					if(finalChild.renderer.IsVisibleFrom(Camera.main) == false){
						//Get the first child position.
						Transform initialChild = backgroundPart.FirstOrDefault();
						Vector3 initialPosition = initialChild.transform.position;
						Vector3 initialSize = (initialChild.renderer.bounds.max - initialChild.renderer.bounds.min);
						finalChild.position = new Vector3(initialPosition.x - initialSize.x, finalChild.position.y, finalChild.position.z);
						backgroundPart.Remove(finalChild);
						backgroundPart.Insert(0, finalChild);
					}
				}
			}
		}
	}
}