using UnityEngine;
using System.Collections;

public class Difficulty : StartingInventory {
	
	enum Diff {beginner = 5, amateur = 3, ranger = 1, expert = 0};
	
	struct difLevel {
		string name;
		int numItems;
	};
	
	int numChoices = 4;
	
	difLevel[] levels;
		
	// default to easy level
	int diffchoice = (int)Diff.beginner;
	
	// Use this for initialization
	void Start () {
		levels = new difLevel[numChoices];
	}
	
	// Update is called once per frame
	void Update () {
		GUILayout.BeginArea(new Rect(Screen.width/2 - 100, Screen.height/2 - 100, 200, 200));
		
		GUILayout.Box ("Difficulty Level");
		
		/*
		foreach ( option in levels) {
			if (GUILayout.Button(option.name)) {
				diffchoice = option;
			}
		}*/
	
		GUILayout.EndArea();
	
	}
	
	public int GetLevel() {
		
		return diffchoice;
		
	}
	
}
