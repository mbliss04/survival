using UnityEngine;
using System.Collections;

public class GUIMenu : MonoBehaviour {
	
	public GUISkin mySkin;
	
	private EMENU_STATE menuState;
	
	private static int PADDING_X 		= 20;
	private static int SPACING_Y 		= 50;
	private static int BUTTON_WIDTH 	= 400;
	private static int BUTTON_HEIGHT 	= 50;
	private static int TITLE_WIDTH 		= 600;
	private static int TITLE_HEIGHT 	= 150;
	
	enum EMENU_STATE{Main, DifficuiltySelect, Exit, StartGame};

	// Use this for initialization
	void Start () {
		menuState = EMENU_STATE.Main;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		// Assign the skin to be the one currently used.
		GUI.skin = mySkin;
		
		switch(menuState){
			case EMENU_STATE.Main:
				// Make a button. This will get the default "button" style from the skin assigned to mySkin.
				if(GUI.Button (new Rect ((Screen.width/2) - BUTTON_WIDTH/2, Screen.height/2 				, BUTTON_WIDTH, BUTTON_HEIGHT), "Start Game")){
					menuState = EMENU_STATE.DifficuiltySelect;
				}
				if(GUI.Button (new Rect ((Screen.width/2) - BUTTON_WIDTH/2, (Screen.height/2) + SPACING_Y	, BUTTON_WIDTH, BUTTON_HEIGHT), "Quit")){
					menuState = EMENU_STATE.Exit;
				}
				break;
				
			case EMENU_STATE.DifficuiltySelect:
				if(GUI.Button (new Rect ((Screen.width/2) - BUTTON_WIDTH/2, Screen.height/2				, BUTTON_WIDTH, BUTTON_HEIGHT), "Easy")){
					menuState = EMENU_STATE.StartGame;
				}
				if(GUI.Button (new Rect ((Screen.width/2) - BUTTON_WIDTH/2, Screen.height/2	+ SPACING_Y, BUTTON_WIDTH, BUTTON_HEIGHT), "Medium")){
					menuState = EMENU_STATE.StartGame;
				}
				if(GUI.Button (new Rect ((Screen.width/2) - BUTTON_WIDTH/2, (Screen.height/2) + SPACING_Y*2	, BUTTON_WIDTH, BUTTON_HEIGHT), "Hard")){
					menuState = EMENU_STATE.StartGame;
				}				
				if(GUI.Button (new Rect ((Screen.width/2) - BUTTON_WIDTH/2, (Screen.height/2) + SPACING_Y*3	, BUTTON_WIDTH, BUTTON_HEIGHT), "Back")){
					menuState = EMENU_STATE.Main;
				}
				break;

			case EMENU_STATE.Exit:
				// Quit the program
				Application.Quit();
			
				break;
							
		
			case EMENU_STATE.StartGame:
				// Quit the program
				Application.LoadLevel("Level1");
			
				break;
			default:
				// Should never be hit
				Debug.Log("Menu state does not exist");
				break;
		}
	}
	
	void Close() {
		Application.Quit();
	}
}
