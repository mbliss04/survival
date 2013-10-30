using UnityEngine;
using System.Collections;

public class PlayerLocator : MonoBehaviour {
	
	public GameObject player;
	private bool playerFound;

	// Use this for initialization
	void Start () {
		playerFound = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if(player.collider.Equals(other)){
			playerFound = true;
		}
	}
	
	public bool isPlayerFound(){
		return playerFound;
	}
}
