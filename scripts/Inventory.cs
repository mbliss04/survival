using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	
	public List<ItemClass> backpack = new List<ItemClass>();
	public List<ItemClass> wearing = new List<ItemClass>();
	int numItems = 0;
	float curWeight = 0f;
	float maxWeight = 75f;
	bool isFull = false;
	
	public Texture inventoryWindow;
	bool showInv = false;
	
	// Use this for initialization
	void Awake () {
		numItems = backpack.Count;
	}
	
	// Update is called once per frame
	void Update () {
		// if press the i button, toggle the inventory
		if (Input.GetKeyDown(KeyCode.Escape)) {
			showInv = !showInv; 
		}
	}
	
	void OnGUI () {
		if (showInv) {
			GUILayout.BeginArea(new Rect(0, Screen.height - 100, Screen.width, 100));
			GUILayout.BeginHorizontal();
			for (int i = 0; i < numItems; i++) {
				Texture2D image = (Texture2D)Resources.Load(backpack[i].Image);
				if (GUILayout.Button(image)) {
					if (backpack[i].Wearable) {
						Debug.Log ("You can put this on");
					}
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			for (int i = 0; i < numItems; i++) {
				GUILayout.Box(backpack[i].Name);
			}
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
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
	
	public void addFood(string typeOfFood, float amount) {
	
		ItemClass newItem = new ItemClass(typeOfFood, amount);
		
		
	}
	
}