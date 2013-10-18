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
	private int layerObject = 1<<8;
	private int food, harvestedTimes, rayCastDistance;
	private int berries, grapes, pGrapes, pBerries;
	private string objectName = "";
	
	// Use this for initialization
	void Start () {
		rayCastDistance 		= 3;
	}
	
	// Update is called once per frame
	void Update () {
		objectName = "";

		// sends out a ray from the camera
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
			
		// checks if ray hits an object on layer 8
		if (Physics.Raycast(ray, out hit, rayCastDistance)) {
			objectName = hit.transform.gameObject.name;
			GameObject c = hit.transform.gameObject;
			
			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				
				// Add to inventory
				if(objectName.Contains("food"))
				{
					//c.GetComponent<Harvest>().getHarvestType();
					harvestFood(c.GetComponent<Harvest>().getHarvestType(), c.GetComponent<Harvest>().getHarvest());
					
					// Now destroy the object.
					Destroy(c);
					
					harvestedTimes++;
				}
			}
		}
	}
	
	void OnGUI () {
		// Make a background box
		GUI.Label (new Rect (20,300,400,80), "Object Name: " + objectName);
		GUI.Label (new Rect (20,315,400,80), "Times Harvested: " + harvestedTimes);
		GUI.Label (new Rect (20,330,400,80), "berries: " + berries);
		GUI.Label (new Rect (20,345,400,80), "grapes: " + grapes);
		GUI.Label (new Rect (20,360,400,80), "pBerries: " + pBerries);
		GUI.Label (new Rect (20,375,400,80), "pGrapes: " + pGrapes);
	}
	
	public void harvestFood(EHarvestType type, int iFoodAmount){
		//food += iFoodAmount;
		
		switch(type){
		case EHarvestType.EBerries:
			berries += iFoodAmount;
			break;
		case EHarvestType.EGrapes:
			grapes += iFoodAmount;
			break;
		case EHarvestType.EPoisionessGrapes:
			pGrapes += iFoodAmount;
			break;
		case EHarvestType.EPoisionessBerries:
			pBerries += iFoodAmount;
			break;
		default:
			Debug.Log("Raycast, HarvestFood; Error harvestType");
			break;
		}	
	}
}
