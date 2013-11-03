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

public class Raycast : MonoBehaviour {
	
	// all objects on layer8 (Object layer)
	private int layerObject = 1<<8;
	
	// interaction with food
	private int food, harvestedTimes, rayCastDistance;
	private int berries, grapes, pGrapes, pBerries;
	
	private string objectName = "";
	
	Inventory playerInv;
	
	void Awake() {
	
		// get access to players inventory to add items
		playerInv = gameObject.GetComponent<Inventory>();
		rayCastDistance = 3;
		
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			
		// sends out a ray from the camera
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;
			
		// checks if ray hits an object on layer 8
		if (Physics.Raycast(ray, out hit, rayCastDistance, 1<<8)) {
			objectName = hit.transform.gameObject.name;
			GameObject c = hit.transform.gameObject;

			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				if (objectName.Contains("food")) {
					addFoodObj(c);
				}
			}
		}
	}
	
	void addFoodObj(GameObject c) {
		// get name of item hit
		string typeOfFood = c.GetComponent<Harvest>().getName();
		int amount = c.GetComponent<Harvest>().getHarvest();
					
		// add to inventory
		playerInv.addFood(typeOfFood, amount);
					
		// Now destroy the object.
		Destroy(c);
					
		harvestedTimes++;
	}
	
}
