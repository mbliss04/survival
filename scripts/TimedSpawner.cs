using UnityEngine;
using System.Collections;

public class TimedSpawner : MonoBehaviour {
	// Enums
	public enum Difficuilty{Easy, Medium, Hard};
	
	// Public Variables
	public GameObject objectToSpawn;
	public Difficuilty difficuiltySetting;
	
	private float DIFFICULTY_EASY 		= 15.0f;
	private float DIFFICULTY_MEDIUM 	= 30.0f;
	private float DIFFICULTY_HARD 		= 45.0f;
	
	
	// Private Variables
	private bool spawned;
	
	// Static Variables
	private static float SECONDS_PER_MINUTE = 1.0f;

	// Use this for initialization
	void Start () {
		spawned = false;
		
		// Make the object inactive.
		objectToSpawn.SetActive(false);
		
		// Set the diccicuilty
		setDifficuiltySetting(difficuiltySetting);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setDifficuiltySetting(Difficuilty newSetting){
		difficuiltySetting = newSetting;
		
		switch(newSetting){
			case Difficuilty.Easy:
				this.Invoke("spawnObject", DIFFICULTY_EASY * SECONDS_PER_MINUTE);
				break;
			
			case Difficuilty.Medium:
				this.Invoke("spawnObject", DIFFICULTY_MEDIUM * SECONDS_PER_MINUTE);
				break;
			
			case Difficuilty.Hard:
				this.Invoke("spawnObject", DIFFICULTY_HARD * SECONDS_PER_MINUTE);
				break;
			
			default:
				Debug.Log("Difficulty Setting failure");
				break;
		}
	}
	
	private void spawnObject(){
		objectToSpawn.SetActive(true);
		
		spawned = true;
	}
	
	public bool isSpawned(){
		return this.spawned;
	}
	
	public GameObject getObject(){
		return this.objectToSpawn;	
	}
}
