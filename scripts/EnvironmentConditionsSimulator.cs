using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////////////////////
//  			**** Enviromental Conditions Simulation ****
//	
//	This script simulates whether conditions, temaptures, humidity 
//	and rainfull. It does this by calculating the amount of sunlight 
//	present in a scene based on the time of day, not actual ingame 
//	light amounts. This script also keeps track of the time of day,
// 	hours, and amount of days that have gone by throughout the
// 	simulation. Animation to do with the movement of the sun is also
//	simulated within the script to coincide with the changing conditions.
//
//  @Author Danny Dryden
//  @Version 0.1
//  @Revision History
//			3/10/13 - Danny Dryden - Script Creation
//			----------------------------------------
//			----------------------------------------
//			----------------------------------------
//////////////////////////////////////////////////////////////////////////////

public class EnvironmentConditionsSimulator : MonoBehaviour {

	///////////////////////
	//  Variables
	///////////////////////
	
	// Whether Properties
	public float  TempatureMinInCelcius;
	public float  TempatureMaxInCelcius;
	private float humidity 							= 100.0f;
	private float tempatureScale 					= 25.0f;
	private float tempature 						= 0.0f;
	private float percentageOfSunLight 				= 0.0f;
	private bool  rain 								= false;
	
	// Time
	private float  minutes 							= 0.0f;		
	private float  hours 							= 0.0f;		
	private float  startTime 						= 6.0f;		// Start at 6am.
	private float  daysPassed 						= 1.0f;
	private bool   isDayTime 						= true;
	private float  frameCounter 					= 0;
	
	// Controllers
	public GameController gameController;
	
	///////////////////////
	// Static Variables
	///////////////////////
	
	// Day Length
	private static float  hoursPerDay 				= 24.0f;
	
	// Tempature Mins/Max's
	private static float tempMin		 			= 0.0f;
	private static float tempMax 					= 100.0f;
	private static float tempChangeOverTime			= 0.05f;
	
	// Sunrise
	private static float sunrise 					= 5.0f;
	private static float sunset 					= 20.0f;
	
	private static int FRAMES_PER_SECOND 			= 15;
	private static int MINUTES_PER_HOUR				= 60;
	
	///////////////////////
	//  Enums
	///////////////////////
	private enum EHumidity{None=0, Small=25, Medium=50, Large=75, Max=100};

	///////////////////////
	//  Initialization
	///////////////////////
	void Start () 
	{
		hours = startTime;
		simulateHumidity();
	}

	///////////////////////
	//  Update loop - Games runs @ 30FPS
	///////////////////////
	void Update () 
	{
		// Accumulate deltaTick.
		frameCounter++;
		
		// Simulate time of day
		// 1 second ='s 1 minute in game time.
		if(frameCounter>=FRAMES_PER_SECOND){
			minutes++;
			
			if(minutes >= MINUTES_PER_HOUR){
				timeOfDay();
				minutes = 0;
			}
			
			// Simulate amount of sunlight.
			simulatePercentageOfSunLight();
			
			// Simulate tempature changing throughout the day.
			simulateTempature();
			
			// Reset deltaCounter
			frameCounter = 0;
		}
	}

	void OnGUI () 
	{
		// Make a background box
		GUI.Label (new Rect (20,170,400,80), "Tempature in Celcius: " 	+ tempature.ToString());
		GUI.Label (new Rect (20,185,400,80), "Humidity: " 				+ humidity.ToString() + "%");
		GUI.Label (new Rect (20,200,400,80), "Day: " 					+ daysPassed.ToString());
		GUI.Label (new Rect (20,215,400,80), "Time: " 					+ hours.ToString() + ":" + minutes.ToString());
		GUI.Label (new Rect (20,230,400,80), "isDaytime: " 				+ isDayTime.ToString());
		GUI.Label (new Rect (20,245,400,80), "Percentage of Sunlight: " + percentageOfSunLight.ToString() + "%");
	}

	public void simulateHumidity()
	{
		// Every day randomize the humidity
		int random = Random.Range(0,4);
		
		switch(random){
			case 0: 
				humidity = (float)EHumidity.None;
				break;
				
			case 1: 
				humidity = (float)EHumidity.Small;
				break;	
				
			case 2: 
				humidity = (float)EHumidity.Medium;
				break;	
				
			case 3: 
				humidity = (float)EHumidity.Large;
				break;
				
			case 4: 
				humidity = (float)EHumidity.Max;
				break;
				
			default:
				humidity = (float)EHumidity.None;
				break;
		}
	}
	
	
	private void simulateTempature()
	{
		// First check if it is daytime or not.
		if(isDayTime){
			// Before suns highest point in the sky rapidly heat the tempature up.
			if(hours <= sunrise + ((sunset-sunrise)/2)){
				tempatureScale += tempChangeOverTime*2;
				
				if(tempatureScale>= tempMax){
					tempatureScale = tempMax;
				}
			}
			else{
				// After lunch slowly cool down
				tempatureScale -= tempChangeOverTime;				
				
				if(tempatureScale<= tempMin){
					tempatureScale = tempMin;
				}
			}
		}
		else{			
			// If before midnight (rapidly dropping tempatures)
			if(hours >= sunset || hours <= hoursPerDay){
				// Use nighttime scale factor so the tempature drops rapidly.
				tempatureScale -= tempChangeOverTime*2;
				
				// Do not let the tempature go below the minNightTimeTemp
				if(tempatureScale < tempMin){
					tempatureScale = tempMin;
				}
			}
			// After midnight (warminig up slowly)
			else{
				// Use daytime scale factor so that it warms up very slowly.
				tempatureScale += tempChangeOverTime/2;
			}
		}
		
		// Now figure out the tempature relative to the Min and 
		// Max degrees celcius that this forest can achieve.
		float range = TempatureMaxInCelcius - TempatureMinInCelcius;
		float aditionalTemp = range * (tempatureScale/100);
		tempature = TempatureMinInCelcius + aditionalTemp;
	}
	
	private void timeOfDay()
	{
		// Keep track of the hours in the day.
		hours++;
		
		if(hours >= hoursPerDay){
			// Keep track of days passed
			daysPassed++;
						
			// Simulate a new days humidity.
			simulateHumidity();
		
			// Reset the hours to midnight.
			hours = 0;
		}
	}
	
	private void simulatePercentageOfSunLight()
	{
		float range = (sunset - sunrise) / 2;
		
		// Before suns highest point
		if(hours > sunrise && hours < sunrise + range){
			// Morning
			isDayTime = true;
			
			// Morning range of sunlight. Default: 6am till 2pm before sun starts to set.
			if(hours-sunrise > 0.0f){
				percentageOfSunLight = ((hours - sunrise) / range) * 100;
			}
			else{
				percentageOfSunLight = 0.0f;
			}
		}
		// After midday
		else if(hours < sunset && hours > sunrise + range){
			// Afternoon
			isDayTime = true;

			if(hours < sunset){
				// Invert percentage
				float startDecreasingTempAt = sunrise + range; // 8
				float settingTime = hours - startDecreasingTempAt; // 4 
				float inversePercentage = ((startDecreasingTempAt - settingTime) / startDecreasingTempAt) * 100;
				percentageOfSunLight = inversePercentage;
			}
			else{
				percentageOfSunLight = 0.0f;
			}
		}
		else{
			percentageOfSunLight = 0;
			
			// Nightime
			isDayTime = false;
		}
	}
	
	public bool getIsDayTime(){
		return isDayTime;
	}
	
	public float getPercentageOfLight(){
		return percentageOfSunLight;
	}
	
	public float getTempature(){
		return tempature;
	}
	
	public float getTempaturePercentage(){
		return tempatureScale;
	}
	
	public float getHumiditye(){
		return humidity;
	}
	
	public float[] getTime(){
		float[] time = new float [2];
		time[0] = hours;
		time[1] = minutes;

		return time;
	}
}