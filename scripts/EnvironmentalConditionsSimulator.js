#pragma strict

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

///////////////////////
//  Variables
///////////////////////

// Whether Properties
var humidity = 100.0;
var tempature = 100.0;
var rain = false;
var amountOfSunLight = 0.0;

// Delta Time
var deltaTimeHours = 0.0;
var deltaTimeEnviromentChanges = 0.0;

// Callendar
var hour = 6.0;							// Start at midnight
var daysPassed = 0.0;
var isDayTime = true;

///////////////////////
// Static Variables
///////////////////////

// Day Length
var dayLength = 240.0; 					// 4 minutes
var nightLength = 60.0;					// 1 minute
var hoursPerDay = 24.0;
var deltaPerHour =  5.0;				// dayLength+nightLength / hoursPerDay
var deltaPerEnviromentChange = 0.1; 	// Time between temp/humidty updates.

// Tempature Mins/Max's
var nightTimeTempMin = 0.0;
var nightTimeTempMax = 30.0;
var dayTimeTempMin = 20.0;
var dayTimeTempMax = 100.0;

// Scaling Factors
var TempDaytimeScaleFactor = 0.3;
var TempNighttimeScaleFactor = 0.3;
var HumidityScaleFactor = 0.1;

// Sunrise
var sunrise = 5.0;
var sunset = 20.0;

var counter = 0.0;

///////////////////////
//  Initialization
///////////////////////
function Start () {
	//deltaPerHour = (dayLength + nightLength) / hoursPerDay;
}

///////////////////////
//  Update loop
///////////////////////
function Update () {
	// Accumulate deltaTick.
	deltaTimeHours += Time.deltaTime;
	deltaTimeEnviromentChanges += Time.deltaTime;
	counter = deltaTimeHours;
	
	// Simulate time of day
	if(deltaTimeHours >= deltaPerHour){
		timeOfDay();
		
		// Reset out delta tick to count for the next 60 seconds.
		deltaTimeHours = 0.0;
	}
	
	if(deltaTimeEnviromentChanges >= deltaPerEnviromentChange){
		
		// Simulate amount of sunlight.
		simulateAmountOfSunLight();
		
		// Simulate tempature changing throughout the day.
		simulateTempature();
		
		// Simulate humidity.
		simulateHumidity();
		
		// Reset delta
		deltaTimeEnviromentChanges = 0.0;
	}
}

function OnGUI () {
	// Make a background box
	GUI.Label (Rect (20,40,200,80), "Tempature: " + tempature.ToString());
	GUI.Label (Rect (20,55,200,80), "Humidity: " + humidity.ToString());
	GUI.Label (Rect (20,70,200,80), "Day: " + daysPassed.ToString());
	GUI.Label (Rect (20,85,200,80), "Hour: " + hour.ToString());
	GUI.Label (Rect (20,100,200,80), "isDaytime: " + isDayTime.ToString());
	GUI.Label (Rect (20,115,200,80), "Amount of Sunlight: " + amountOfSunLight.ToString());
}

function simulateHumidity(){

}


function simulateTempature(){
	// First check if it is daytime or not.
	if(isDayTime){
		// Simulate a random tempature based upon how much sunlight there is.
		// "amountOfSunLight" provides us with a percentage that we can use to
		// figure out how much time has passed through the day cycle. 50% being
		// the highest point the sun reaches in the sky.
		
		// We use our "vairiationScaleFactor" in conjunction with Random num
		// gen to give a varing tempature constantly, between the Min and Max's
		// of a day and night tempatures. The Tempature at any point can vary 
		// between 0% and 10% of the base tempature that it should be.
		var scale = (amountOfSunLight * Random.Range(0.0, TempDaytimeScaleFactor) / 100);
		
		tempature = ((dayTimeTempMax - dayTimeTempMin) * scale) + dayTimeTempMin;
	}
	else{
		// At nightime, we take the last daytime tempature, or last tempature 
		// recorded at this time of night, and take away between 0% and 30% of
		// its total down to the minumunNightTemp and no more, except for when
		// a new day has begun and everything is begining to warm up to a new sunrise.
		
		// If before midnight (rapidly dropping tempatures)
		if(hour >= sunset || hour <= hoursPerDay){
			// Use nighttime scale factor so the tempature drops rapidly.
			tempature = tempature - (tempature * Random.Range(0.0, TempNighttimeScaleFactor));
			
			// Do not let the tempature go below the minNightTimeTemp
			if(tempature > nightTimeTempMin){
				tempature = nightTimeTempMin;
			}
		}
		// After midnight (warminig up slowly)
		else{
			// Use daytime scale factor so that it warms up very slowly.
			tempature = tempature + (tempature * Random.Range(0.0, TempDaytimeScaleFactor));
			
			// Do not let the tempature go above the MaxNightTimeTemp
			if(tempature > nightTimeTempMax){
				tempature = nightTimeTempMax;
			}
		}
	}
}

function animateSunMovement(){
	// TODO
}

function timeOfDay(){
	// Keep track of the hours in the day.
	hour++;
	
	if(hour >= hoursPerDay){
		// Keep track of days passed
		daysPassed += 1;
	
		// Reset the hours to midnight.
		hour = 0;
	}
}

function simulateAmountOfSunLight(){
	if(hour > sunrise && hour <= sunset){
		// Figure out generic values, but scale them to large numbers to get more percise values.
		var dayLightHours = (sunset - sunrise) * 0.5;
		var hoursSinceLightBroke = (hour - sunrise) * 0.5;
		var sunsHighestPoint = (dayLightHours / 2) * 0.5;
	
		// Is the sun at its peak in the sky yet
		if(hoursSinceLightBroke < sunsHighestPoint){
		
			// Figure our the ratio between the sunrise and 
			// sunset to get a average amount of sunlight.
			amountOfSunLight = ((hoursSinceLightBroke / dayLightHours) * 2) * 100;
		}
		else{
			amountOfSunLight = ((hoursSinceLightBroke / dayLightHours) * 2) * 100;
		}
		
		// Daytime
		isDayTime = true;
	}
	else{
		amountOfSunLight = 0;
		
		// Nightime
		isDayTime = false;
	}
}