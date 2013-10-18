using UnityEngine;
using System.Collections;

public class GUIMenu : MonoBehaviour {
	
	public GUISkin mySkin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		// Assign the skin to be the one currently used.
		GUI.skin = mySkin;

		// Make a button. This will get the default "button" style from the skin assigned to mySkin.
		GUI.Button (new Rect (10,10,150,20), "Skinned Button");
	}
}
