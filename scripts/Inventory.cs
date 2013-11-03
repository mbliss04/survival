using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	
	public List<ItemClass> backpack = new List<ItemClass>();
	public List<ItemClass> wearing = new List<ItemClass>();
	float curWeight = 0f;
	float maxWeight = 10f;
	
	// backpack booleans
	bool isFull = false;
	bool hasBerries = false;
	bool hasGrapes = false;
	
	public Texture inventoryWindow;
	bool showInv = false;
	
	// Use this for initialization
	void Start () {
		
		//Screen.showCursor = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		// if press button, toggle the inventory
		if (Input.GetKeyDown(KeyCode.I)) {
			showInv = !showInv;
		}
		if (showInv) {
			// hide cursor
			Screen.showCursor = true;
		}

	}
	
	void OnGUI () {
		if (showInv) {
			GUILayout.BeginArea(new Rect(0, Screen.height - 100, Screen.width, 100));
			GUILayout.BeginHorizontal();
			for (int i = 0; i < backpack.Count; i++) {
				Texture2D image = (Texture2D)Resources.Load(backpack[i].Image);
				if (GUILayout.Button(image)) {
					if (backpack[i].Wearable) {
						dress(i);
					}
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			for (int i = 0; i < backpack.Count; i++) {
				GUILayout.Box(backpack[i].Name);
			}
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}
		
	}
	
	public int Items {
		get { return backpack.Count; }
	}
	
	public bool Full {
		get { return isFull; }
		set { isFull = value; }
	}
	
	public bool addInventoryItem(ItemClass newItem) {
		
		// if there is room, add item to the backpack
		if ((newItem.Weight + curWeight) < maxWeight) {
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
	
	public void addFood(string typeOfFood, int amount) {
	
		int index = -1;
		index = backpack.FindIndex(x => x.Name.Contains(typeOfFood));
		if (index >= 0) {
			// add new quantity to existing item
			backpack[index].Quantity += amount;
		}
		else {
			Debug.Log ("adding " + typeOfFood);
			// make a new item and add it to the inventory
			ItemClass newitem = new ItemClass(typeOfFood, (float)amount*.1f);
			newitem.Image = typeOfFood;
			newitem.Quantity = amount;
			newitem.Wearable = false;
			newitem.Viewable = false;
			newitem.Flammable = false;
			newitem.Chosen = false;
			addInventoryItem(newitem);
		}
		
	}
	
	void dress(int index) {
	
		int wearIndex = wearing.FindIndex(x => x.Name.Contains(backpack[index].Name));
		// if is already wearing, remove from wearing list
		if (wearing.Count != 0 && wearIndex >= 0) {
			wearing.RemoveAt(wearIndex);
		}
		// otherwise add to wearing list
		else {
			wearing.Add(backpack[index]);
		}
	
	}
	
}