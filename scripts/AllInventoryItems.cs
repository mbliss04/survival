using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class AllInventoryItems : MonoBehaviour {
	
	public enum itemProp {name, descrip, quantity, weight, image, flam, view, wear};
		
	List<ItemClass> allitems = new List<ItemClass>();
	
	bool success = false;
	
	string filename = "C:/Users/mccallbliss/games/items.txt";
	
	void Awake() {
		
		// reads in data from text file
		success = readInData();
		if (success) {
			Debug.Log ("all read in");
		}
		
	}
	
	ItemClass[] itemlist;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public ItemClass[] randomList(int numItems) {
		
		// index of the item in the big list
		int index = 0;
		
		// make a list of random items
		itemlist = new ItemClass[numItems];
		
		// loop through big list and make 
		// array of random items
		for (int i = 0; i < numItems; i++) {
			index = Random.Range(0, allitems.Count);
			do {
				index = Random.Range(0, allitems.Count);
			}  while (!allitems[index].Chosen);
			itemlist[i] = allitems[index];
			allitems[index].Chosen = true;
		}
		
		return itemlist;
		
	}
	
	bool readInData() {
		
		StreamReader reader = new StreamReader(filename, Encoding.Default);
		string line;
		int i = 0;
		
		using (reader) {
			
			do {
				line = reader.ReadLine();
				if (line != null) {
					string[] qualities = line.Split (',');
					if (qualities.Length > 0) {
						Debug.Log (qualities[(int)itemProp.name]);
						/*
						allitems[i].Name = qualities[(int)itemProp.name];
						allitems[i].Description = qualities[(int)itemProp.descrip];
						allitems[i].Quantity = qualities[(int)itemProp.quantity];
						allitems[i].Weight = qualities[(int)itemProp.weight];
						allitems[i].Image = qualities[(int)itemProp.image];
						allitems[i].Flammable = qualities[(int)itemProp.flam];
						allitems[i].Viewable = qualities[(int)itemProp.view];
						allitems[i].Wearable = qualities[(int)itemProp.wear];
						allitems[i].Index = i;
						*/
					}
				}
				i++;
			} while (line != null);
		}
		
		reader.Close();
		return true;
		
	}
	
}
