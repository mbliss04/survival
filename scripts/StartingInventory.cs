/* -----------------------------
 * StartingInventory.cs
 * Author: McCall Bliss
 * Created: Oct 1, 2013
 * Last Modified: Oct 3, 2013
 * 
 * 
 * Acts as a game user interface for the 
 * start of the game, allowing the player
 * to choose from a list of randomized items
 * and add them to their inventory
 * --------------------------------------*/

using UnityEngine;
using System.Collections;

public class StartingInventory : MonoBehaviour {
	
	// Variables

	int numItems = 5;
	int difficulty = 1;
	
	// list of random starting items
	ItemClass[] itemlist;
	
	// Scripts
	protected Inventory playerInventory;
	
	protected GameObject itemobject;

	protected AllInventoryItems allitems;

	// Initialize function
	void Start() {
		
		// get inventory game object
		itemobject = GameObject.Find ("InventoryObjects");
		
		// get script components
		if (playerInventory) {
			playerInventory = gameObject.GetComponent<Inventory>();
		}
		if (itemobject) {
			allitems = itemobject.gameObject.GetComponent<AllInventoryItems>();	 
		}
			
		// Get difficulty from user
		// int difficulty = getDiff();
		
		// Get number of items allowed
		//numItems = getNumItems(difficulty);
		
		// get random list of items
		itemlist = allitems.getRandomList();
		
	}
	
	// Draw user interface
	void OnGUI () {
		// Make a background box
		GUILayout.BeginArea(new Rect(Screen.width/2 - 100, Screen.height/2 - 100, 200, 200));
		
		GUILayout.Box ("Starting Inventory");
		
		/*
		foreach (ItemClass item in itemlist) {
			if (GUILayout.Button(item.Name)) {
				playerInventory.backpack.Add(item);
				// Remove item from screen/choices
				// Refresh buttons
			}
		}*/
		
		GUILayout.EndArea();
	}
	
}
