using UnityEngine;
using System.Collections;

public class PlayerAttributes : MonoBehaviour {
	
	enum attr {hunger, warmth, sanity, thirst, energy, health};
	
	int numAttr = 6;
	float maxAmount = 100f;
	
	float[] attributes;

	bool playerAlive = true;
	bool playerAsleep = false;
	
	// Other objects that effect Attributes
	public GameObject weather;
	protected CharacterMotor movement;
	
	void Awake() {
		
		weather = GameObject.Find("Weather Conditions");
		movement = gameObject.GetComponent<CharacterMotor>();
		
		// create attributes array and assign initial value
		attributes = new float [numAttr];
		for (int i = 0; i < numAttr; i++) {
			attributes[i] = maxAmount;
		}
		
	}
	
	
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		updateAttributes();
		checkLifeStatus();
		if (!playerAlive) {
			endGame();
		}
	}
	
	// updates all player attributes
	void updateAttributes() {
		Hunger ();
		Warmth ();
		Sanity ();
		Thirst ();
		Energy ();
		Health ();
	}
	
	// updates hunger attribute
	void Hunger() {
		
		// decreases hunger naturally during day
		decrease(attr.hunger, 0.0005f);
	
	}
	
	// updates warmth attribute
	void Warmth() {
		
		float amount = 0f;
		
		// if night time
			
		decrease(attr.warmth, amount);

	}
	
	// updates sanity attribute
	void Sanity() {
		
		// decreases sanity naturally during day
		decrease(attr.sanity, 0.000005f);

	}
	
	// updates thirst attribute
	void Thirst() {
		
		// decreases thirst naturally during day
		decrease(attr.thirst, 0.005f);

	}

	// updates energy attribute
	void Energy() {
		
		// if the character jumps, decrease energy by more
		if (movement.IsJumping()) {
			decrease(attr.energy, 0.05f);
		}
		
		// decreases energy naturally during the day if player is not sleeping
		if (!playerAsleep) {
			decrease(attr.energy, 0.0005f);
		}
		else {
			while (attributes[(int)attr.energy] < maxAmount) {
				decrease(attr.energy, -0.005f);
			}
		}
		
	}
	
	// updates health attribute
	void Health() {
		
		

	}
	
	// allows other scripts to affect the health condition
	public void affectHealth(float increment) {
		
		decrease(attr.health, increment);
	
	}
	
	// decreases each attribute a bit with the passage of time
	void decrease(attr name, float amount) {
	
		attributes[(int)name] += -(amount * Time.deltaTime);
		
	}
	
	// changes life status based on attribute levels
	void checkLifeStatus() {
	
		// determines if player is dead
		for (int i = 0; i < numAttr; i++) {
			if (attributes[i] < 0) {
				playerAlive = false;
			}
		}
		
	}
	
	// ends the game when player dies
	void endGame() {
		
		
	
	}
}
