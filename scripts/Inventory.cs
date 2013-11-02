using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	
	public List<ItemClass> backpack = new List<ItemClass>();
	int numItems = 0;
	float curWeight = 0f;
	float maxWeight = 75f;
	bool isFull = false;
	
	public Texture inventoryWindow;
	bool showInv = false;
	
	// Use this for initialization
	void Awake () {
		numItems = backpack.Count;
		Object[] textures = Resources.LoadAll("Textures", typeof(Texture2D));
		Debug.Log (textures);
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
			GUILayout.BeginArea(new Rect(0, 0, Screen.width, 50));
			GUILayout.BeginHorizontal();
			for (int i = 0; i < numItems; i++) {
				GUILayout.Button(backpack[i].texture);
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
			Texture2D temptex = (Texture2D)Resources.LoadAssetAtPath(newItem.Image, typeof(Texture2D));
			Debug.Log (temptex);
			newItem.texture = temptex;
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
