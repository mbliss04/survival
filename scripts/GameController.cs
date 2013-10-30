using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	// Public Variables	
	public GUISkin mySkin;
	public GameObject player;
	public GameObject helicopterLocator;
	public GameObject helicopterController;
	public Difficuilty difficuilty;
	
	public enum Difficuilty{Easy, Medium, Hard};
	
	// Private variables
	private bool gameOver;
	private bool gameWon;

	void Start () {
		this.gameOver = false;
		this.gameWon = false;
	}
	
	void Update () {
		// If helicopter is spawned, check to see if it can see the player
		if(helicopterController.GetComponent<TimedSpawner>().isSpawned()){
			if(helicopterLocator.GetComponent<PlayerLocator>().isPlayerFound()){
				// Player won, player has been found by helicopter.
				gameOver = true;
				gameWon = true;
			}		
		}
		if(!player.GetComponent<PlayerAttributes>().getIsPlayerAlive()){
			// Player lost, has died.
			gameOver = true;
			gameWon = false;
		}
	}
	
	public void setDifficuilty(Difficuilty difficuilty){
		this.difficuilty = difficuilty;
	}
	
	void OnGUI(){
		// Assign the skin to be the one currently used.
		GUI.skin = mySkin;
		
		if(gameOver && gameWon){			
			GUI.Box (new Rect ((Screen.width/2)-200, (Screen.height/2)-100			, 400, 100), "You were found and won the game!");
			if(GUI.Button (new Rect ((Screen.width/2)-200, (Screen.height/2) - 50	, 400, 50), "Return to menu")){
				Application.LoadLevel("GUI");
			}	
		}		
		if(gameOver && !gameWon){			
			GUI.Box (new Rect ((Screen.width/2)-200, (Screen.height/2)-100			, 400, 100), "You died and lost the game!");
			if(GUI.Button (new Rect ((Screen.width/2)-200, (Screen.height/2) -50	, 300, 40), "Return to menu")){
				Application.LoadLevel("GUI");
			}	
		}
	}
}
