using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	
	public List<ItemClass> backpack = new List<ItemClass>();
	int numItems;
	float curWeight = 0f;
	float maxWeight = 75f;
	bool isFull = false;
	
	public Texture inventoryWindow;
	
	// Use this for initialization
	void Awake () {
		backpack = null;
		numItems = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI () {
	
		// if the cursor isn't locked, show the inventory
		if (!Screen.lockCursor) {
			GUI.BeginGroup(new Rect(Screen.width / 2, Screen.height - Screen.height, Screen.width, 60));
			for (int i = 0; i < numItems; i++) {
				if (GUI.Button (new Rect (10,10, 50, 50), backpack[i].texture)) {
					// show the functions of that item
				}
			}
			GUI.EndGroup();
		}
		
	}
	
	public int Items {
		get { return numItems; }
		set { numItems = value; }
	}
	
	public bool Full {
		get { return isFull; }
		set { isFull = value; }
	}
	
	public bool addInventoryItem(ItemClass newItem) {
		
		// if there is room, add item to the backpack
		if ((newItem.Weight + curWeight) < maxWeight) {
			newItem.ImgTexture = (Texture2D)Resources.LoadAssetAtPath(newItem.Image, typeof(Texture2D));
			backpack.Add(newItem);
			curWeight += newItem.Weight;
			return !isFull;
		}
		isFull = true;
		return isFull;
		
	}
	
	public void deleteItem(ItemClass toBeDeleted) {
		
		// find item and remove it from inventory
		int index = backpack.FindIndex(r => r.Name == toBeDeleted.Name);
		backpack.RemoveAt(index);
		
	}
	
}
