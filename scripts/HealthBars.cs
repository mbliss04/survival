/* -------------------------------
 * HealthBars.cs
 * Author: McCall Bliss
 * Created: Oct 1, 2013
 * Last Modified: Oct 3, 2013
 * 
 * Takes a list of needs for the player
 * and updates/displays their values
 * --------------------------------*/

/* -------------------------------
 * HealthBars.cs
 * Author: McCall Bliss
 * Created: Oct 1, 2013
 * Last Modified: Oct 3, 2013
 * 
 * Takes a list of needs for the player
 * and updates/displays their values
 * --------------------------------*/

using UnityEngine;
using System.Collections;

public class HealthBars : MonoBehaviour {
	
	// Definition of a need
	struct Need {
		private readonly string name;
		private readonly int height;
		private float amount;
		
		public Need (string name, int height, float amount) {
			this.name = name;
			this.height = height;
			this.amount = amount;
		}
		
		public string Name { get { return name; } }
		public int Height { get { return height; } }
		public float Amount { get { return amount; } }
		public void alterAmount(float newamount) {
			this.amount = newamount;
		}
	}
	
	// Variables
	private int maxHealth = 100;
	static private float startHealth = 100f;
	private int barlength = 150;
	static private int startval = 10;
	static private int addval = 25;
	
	// Creates an array of 
	static Need[] needs = new Need[]{
             new Need ("Health", startval, startHealth),
             new Need ("Thirst", startval+addval, startHealth),
             new Need ("Hunger", startval+addval*2, startHealth), 
			 new Need ("Warmth", startval+addval*3, startHealth), 
			 new Need ("Sanity", startval+addval*4, startHealth)
   			};
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		naturallyDecrease();	
	}
	
	void OnGUI() {
		// Draws a bar for each need
		foreach (Need item in needs) {
			GUI.Box(new Rect(10, item.Height, barlength, 20), item.Name + " " + item.Amount + "/" + maxHealth);
		}
	}
	
	// changes player sanity every update of the game
	void naturallyDecrease() {
		// Health
		needs[0].alterAmount(needs[0].Amount - 0.000001f);
		// Thirst
		needs[1].alterAmount(needs[1].Amount - 0.0001f);
		// Hunger
		needs[2].alterAmount(needs[2].Amount - 0.0000001f);
		// Sanity
		needs[4].alterAmount(needs[4].Amount - 0.000000001f);
	}
	
}
