using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	
	public List<ItemClass> backpack = new List<ItemClass>();
	int numItems;
	float maxWeight = 75f;
	bool isFull = false;
	
	// Use this for initialization
	void Awake () {
		backpack = null;
		numItems = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public int Items {
		get { return numItems; }
		set { numItems = value; }
	}
	
	public bool Full {
		get { return isFull; }
		set { isFull = value; }
	}
	
}
