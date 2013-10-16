using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class AllInventoryItems : ScriptableObject {
	
	public enum itemProp {name, descrip, quantity, weight, image, flam, view, wear};
		
	List<ItemClass> allitems = new List<ItemClass>();
	
	bool success = false;
	
	int numProperties = 7;
	
	int numChoices = 10;
	
	int numItems = 0;
	
	string filename = "Assets/items.txt";
	
	ItemClass[] itemlist;
	
	void Awake() {
		
		// reads in data from text file
		success = readInData();
		if (success) {
			Debug.Log ("all read in");
			generateRandomList();
		}
		
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void generateRandomList() {
		
		// index of larger list
		int index = 0;
		
		// make a new list to store random items
		itemlist = new ItemClass[numChoices];
		
		Debug.Log(numItems);
		
		// loop through big list and make 
		// array of random items
		for (int i = 0; i < numChoices; i++) {
			index = Random.Range(0, allitems.Count);
			do {
				index = Random.Range(0, allitems.Count);
			}  while (!allitems[index].Chosen);
			itemlist[i] = allitems[index];
			allitems[index].Chosen = true;
		}
	
	}
	
	public ItemClass[] getRandomList() {
		
		return itemlist;
		
	}
	
	bool readInData() {
		
		StreamReader reader = new StreamReader(filename, Encoding.Default);
		string line;
		
		using (reader) {
			
			do {
				line = reader.ReadLine();
				if (line != null) {
					string[] qualities = line.Split (',');
					if (qualities.Length > 0 && qualities.Length == numProperties) {
						success = AddNewItem(qualities);
						if (success) {
							Debug.Log ("Added new item");
							numItems++;
						}
					}
				}
			} while (line != null);
		}
		
		reader.Close();
		return true;
		
	}
	
	bool AddNewItem(string[] qualities) {
		ItemClass newitem = new ItemClass();
		newitem.Name = qualities[(int)itemProp.name];
		newitem.Description = qualities[(int)itemProp.descrip];
		newitem.Quantity = System.Int32.Parse(qualities[(int)itemProp.quantity]);
		newitem.Weight = float.Parse(qualities[(int)itemProp.weight]);
		newitem.Image = qualities[(int)itemProp.image];
		newitem.Flammable = bool.Parse(qualities[(int)itemProp.flam]);
		newitem.Viewable = bool.Parse(qualities[(int)itemProp.view]);
		newitem.Wearable = bool.Parse(qualities[(int)itemProp.wear]);
		newitem.Chosen = false;
		newitem.Index = numItems;
		allitems.Add (newitem);
		return true;
	}
	
}
