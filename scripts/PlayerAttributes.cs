using UnityEngine;
using System.Collections;

public class PlayerAttributes : MonoBehaviour {
	
	enum attr {hunger, warmth, sanity, thirst, energy, health};
	
	int numAttr = 6;
	float maxAmount = 100f;
	float goingInsane = 40f;
	float exertion = 0f;
	
	float[] attributes;
	
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
		
		// get the environmental simulator script
		//EnvironmentalConditionsSimulator weatherCond = weather.gameObject.GetComponent<EnvironmentalConditionsSimulator>();
		Debug.Log (weather.gameObject.name);
		
	}
	
	// Update is called once per frame
	void Update () {
		updateAttributes();
		if (!playerAlive()) {
			endGame();
		}
	}
	
	// updates all player attributes
	void updateAttributes() {
		calcExertion();
		Hunger ();
		Warmth ();
		Sanity ();
		Thirst ();
		Energy ();
		//Health ();
	}
	
	// updates hunger attribute
	void Hunger() {
		
		float hungerDecrease = 0.0005f;
		// decreases hunger naturally during day
		decrease(attr.hunger, (hungerDecrease + exertion));
	
	}
	
	public void affectHunger(float amount) {
		
		attributes[(int)attr.hunger] += amount;
		attributes[(int)attr.energy] += amount;
	
	}
	
	// updates warmth attribute
	void Warmth() {
		
		float warmthDecrease = 0f;
		
		//if (weather.isDayTime) {
		
		//}
			
		decrease(attr.warmth, warmthDecrease);

	}
	
	// updates sanity attribute
	void Sanity() {
		
		float sanityDecrease;
		
		// if other attributes are less than certain amount, make
		// sanity go down faster
		if (attributes[(int)attr.thirst] < goingInsane ||
			attributes[(int)attr.hunger] < goingInsane ||
			attributes[(int)attr.health] < goingInsane) {
			sanityDecrease = 0.005f;
		}		
		else {
			sanityDecrease = 0.000005f;
		}
		
		decrease(attr.sanity, (sanityDecrease + exertion));

	}
	
	// updates thirst attribute
	void Thirst() {
		
		float thirstDecrease = 0.005f;
		decrease(attr.thirst, (thirstDecrease + exertion));

	}
	
	public void affectThirst(float amount) {
	
		attributes[(int)attr.thirst] += amount;
		attributes[(int)attr.energy] += amount;
		
	}

	// updates energy attribute
	void Energy() {
		
		float energyDecrease = 0.0005f;
		decrease(attr.energy, (energyDecrease + exertion));
		
	}
	
	// allows other scripts to affect the health condition
	public void affectHealth(float increment) {
		
		decrease(attr.health, increment);
	
	}
	
	// calculates players exertion and modifies exertion variable
	private float calcExertion() {
		
		float exertion = 0f;
		
		if (movement.IsJumping()) {
			exertion += 0.5f;
		}
		if (movement.GetVelocity().magnitude > 0) {
			exertion += 0.5f;
		}
		
		return exertion;
		
	}
	
	// decreases each attribute a bit with the passage of time
	void decrease(attr name, float amount) {
	
		attributes[(int)name] += -(amount * Time.deltaTime);
		
	}
	
	// changes life status based on attribute levels
	public bool playerAlive() {
	
		// determines if player is dead
		for (int i = 0; i < numAttr; i++) {
			if (attributes[i] <= 0) {
				return false;
			}
		}
		
		// player is still in the game
		return true;
		
	}
	
	// ends the game when player dies
	void endGame() {
		
		
	
	}
}
