/* -------------------------------
 * Raycast.cs
 * Author: McCall Bliss
 * Created: Oct 1, 2013
 * Last Modified: Oct 3, 2013
 * 
 * Casts a ray from object and, if 
 * the object is close enough and 
 * interactive, it presents a number
 * of different options to the player
 * --------------------------------*/

using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction*10, Color.cyan);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, 20)) {
			Debug.DrawRay(ray.origin, ray.direction*hit.distance, Color.red);
			Debug.Log("Ray hit " + hit.transform.gameObject.tag);
		}
	}
}
