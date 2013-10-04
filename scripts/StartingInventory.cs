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

	string[] items = {"Maps","Compass","Lighter","Survival Guide","Torch","Med kit","Sun Hat","Outdoor Shoes","Wind proof clothing","Blanket","Drinking Water","Canned Foods","Rope","Sunscreen","Waterproof matches","Cotton balls","Knife","Mirror", "Sanitary Wipes", "Water purification tablets", "Insect repellent","High pitch whistle", "Solar blanket"};

	string[] itemlist;
	

	// Initialize function
	void Start() {
		// Get difficulty from user
		// int difficulty = getDiff();
		int difficulty = 1;
		
		// Get number of items allowed
		numItems = getNumItems(difficulty);
		
		// Make list of randomized starting items
		startingItemList();
	}
	
	// Draw user interface
	void OnGUI () {
		// Make a background box
		GUILayout.BeginArea(new Rect(Screen.width/2 - 100, Screen.height/2 - 100, 200, 200));
		
		GUILayout.Box ("Starting Inventory");
		
		foreach (string item in itemlist) {
			if (GUILayout.Button(item)) {
				// Add item to inventory
				// Remove item from screen/choices
				// Refresh buttons
			}
		}
		
		GUILayout.EndArea();
	}
	
	private int getNumItems(int difficulty) {
		int numGameItems = 5;
		if (difficulty == 2) {
			numGameItems = 3;
		}
		if (difficulty == 3) {
			numGameItems = 1;
		}
		if (difficulty == 4) {
			numGameItems = 0;
		}
		return numGameItems;
	}
	
	void startingItemList() {
		itemlist = new string[numItems];
		string[] temp = new string[items.Length];
		for (int i = 0; i < items.Length; i++) {
			temp[i] = items[i];
		}
		int index;
		for (int i = 0; i < numItems; i++) {
			index = Random.Range(0, temp.Length);
			do {
				index = Random.Range(0, temp.Length);
			}  while (temp[index] == null);
			itemlist[i] = temp[index];
			temp[index] = null;
		}
	}
}
