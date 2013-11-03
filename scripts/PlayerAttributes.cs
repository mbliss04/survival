using UnityEngine;
using System.Collections;

public class PlayerAttributes : MonoBehaviour {
	
	private enum attr {hunger, warmth, sanity, thirst, energy, health};	
	private int numAttr = 6;
	private string[] names = {"Hunger","Warmth","Sanity","Thirst","Energy","Health"};

	private float maxAmount = 100f;
	private float goingInsane = 40f;
	private float exertion = 0f;
	
	public Texture2D background;
	
	private int startheight = 50;
	
	private float[] attributes;
	
	private bool isAlive;
	
	// Other objects that effect Attributes
	protected CharacterMotor movement;
	public EnvironmentConditionsSimulator weather;
	protected Inventory playerInv;
	
	void Awake() {
		//weather = environment.GetComponent<EnvironmentConditionsSimulator>();
		movement = gameObject.GetComponent<CharacterMotor>();
		playerInv = gameObject.GetComponent<Inventory>();
		
		// create attributes array and assign initial value
		attributes = new float [numAttr];
		for (int i = 0; i < numAttr; i++) {
			attributes[i] = maxAmount;
		}
	}
	
	
	// Use this for initialization
	void Start () {
		isAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
		updateAttributes();
		playerAlive();
	}
	
	void OnGUI() {
		// Draws a bar for each need
		GUI.BeginGroup(new Rect(5,5,160,300),background);
		for (int i = 0; i < numAttr; i++) {
			GUI.Box(new Rect(30, 30+(i*20), (int)attributes[i], 20), names[i] + ": " + (int)attributes[i] + "%");
		}
		GUI.EndGroup();
	}
	
	// updates all player attributes
	void updateAttributes() {
		calcExertion();
		Hunger ();
		Warmth ();
		Sanity ();
		Thirst ();
		Energy ();
		keepUnderHundred();
	}
	
	// updates hunger attribute
	void Hunger() {
		float hungerDecrease = 0.25f;
		// decreases hunger naturally during day
		decrease(attr.hunger, (hungerDecrease + exertion));
	}
	
	public void affectHunger(float amount) {
		attributes[(int)attr.hunger] += amount;
		attributes[(int)attr.energy] += amount;
	}
	
	// updates warmth attribute
	void Warmth() {
		float warmthDecrease = 0.2f;
		
		// if not wearing anything, make affected by warmth
		if (playerInv.wearing.Count == 0) {
			if (weather.getTempaturePercentage() < 0.05) {
				decrease(attr.warmth, warmthDecrease);
				decrease(attr.health, warmthDecrease);
				decrease(attr.energy, warmthDecrease);
			}
			else {
				warmthDecrease = 0.05f;
				increase(attr.warmth, warmthDecrease);
			}
		}
	
	}
	
	public void affectWarmth(int amount) {
		
		attributes[(int)attr.warmth] += amount;
		
	}
	
	// updates sanity attribute
	void Sanity() {
		float sanityDecrease;
		
		// if other attributes are less than certain amount, make
		// sanity go down faster
		if (attributes[(int)attr.thirst] < goingInsane ||
			attributes[(int)attr.hunger] < goingInsane ||
			attributes[(int)attr.health] < goingInsane) {
			sanityDecrease = 0.35f;
		}		
		else {
			sanityDecrease = 0.1f;
		}
		
		decrease(attr.sanity, (sanityDecrease + exertion));
	}
	
	// updates thirst attribute
	void Thirst() {
		
		float thirstDecrease = 0.35f;
		decrease(attr.thirst, (thirstDecrease + exertion));

	}
	
	public void affectThirst(float amount) {
		attributes[(int)attr.thirst] += amount;
		attributes[(int)attr.energy] += amount;
		attributes[(int)attr.sanity] += amount;

	}

	// updates energy attribute
	void Energy() {
		float energyDecrease = 0.3f;
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
	
	// increases each attribute a bit with the passage of time
	void increase(attr name, float amount) {
		attributes[(int)name] += (amount * Time.deltaTime);
	}
	
	// changes life status based on attribute levels
	private void playerAlive() {
		// determines if player is dead
		for (int i = 0; i < numAttr; i++) {
			if (attributes[i] <= 0) {
				isAlive = false;
			}
		}
	}
	
	public bool getIsPlayerAlive(){
		return this.isAlive;
	}
	
	public float[] getAttributes(){
		return this.attributes;
	}
	
	void keepUnderHundred() {
		for (int i = 0; i < numAttr; i++) {
			if (attributes[i] > maxAmount) {
				attributes[i] = maxAmount;
			}
		}
	}
}
