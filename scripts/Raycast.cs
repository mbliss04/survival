/* -------------------------------
 * Raycast.cs
 * Author: McCall Bliss
 * Created: Oct 1, 2013
 * Last Modified: Oct 10, 2013
 * 
 * Casts a ray from object and, if 
 * the object is close enough and 
 * interactive, it presents a number
 * of different options to the player
 * --------------------------------*/
 
using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (Component<Halo>))]

public class Raycast : MonoBehaviour {
	
	// all objects on layer8 (Object layer)
	int layerObject = 1<<8;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// sends out a ray from the camera
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction*10, Color.cyan);
		RaycastHit hit;
		
		// checks if ray hits an object on layer 8
		if (Physics.Raycast(ray, out hit, 3, layerObject)) {
			
			Debug.DrawRay(ray.origin, ray.direction*hit.distance, Color.red);
			Debug.Log("Ray hit " + hit.transform.gameObject.tag);
			
			// change color if has been hit
			hit.collider.gameObject.renderer.material.color = Color.red;
			
			if (Input.GetKeyDown(KeyCode.Space)) {
				// add to inventory
			}
		}
		
	}
	
}