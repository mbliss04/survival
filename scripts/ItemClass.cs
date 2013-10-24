using UnityEngine;
using System.Collections;

public class ItemClass {
	
	string itemName;
	string descrip;
	int index;
	int quantity;
	float weight;
	string imageSrc;
	public Texture2D texture;
	
	bool isFlammable;
	bool isViewable;
	bool isWearable;
	bool isChosen;
	
	//default constructor
	public ItemClass() {
	
	}
	
	public ItemClass(string name, float itemweight) {
		
		itemName = name;
		weight = itemweight;
	
	}
	
	
	public string Name {
		get { return itemName; }
		set { itemName = value; }
	}
	
	public string Description {
		get { return descrip; }
		set { descrip = value; }
	}
	
	public int Index {
		get { return index; }
		set { index = value; }
	}
	
	public int Quantity {
		get { return quantity; }
		set { quantity = value; }
	}
	
	public float Weight {
		get { return weight; }
		set { weight = value; }
	}
	
	public string Image {
		get { return imageSrc; }
		set { imageSrc = value; }
	}
	
	public Texture2D ImgTexture {
		get { return texture; }
		set { texture = value; }
	}
	
	public bool Flammable {
		get { return isFlammable; }
		set { isFlammable = value; }
	}
	
	public bool Viewable {
		get { return isViewable; }
		set { isViewable = value; }
	}
	
	public bool Wearable {
		get { return isWearable; }
		set {isWearable = value; }
	}
	
	public bool Chosen {
		get { return isChosen; }
		set { isChosen = value; }
	}
	
}
