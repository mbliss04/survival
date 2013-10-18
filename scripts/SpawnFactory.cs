using UnityEngine;
using System.Collections;

public class SpawnFactory : MonoBehaviour {
	// Private variables
	private Vector3 cameraPosition;
	private Terrain terrain;
	
	// Public variables
	public int iNumDefaultHarvestableObjects, numSpawned;
	public float fMinSpawnDist;
	public float fMaxSpawnDist;
	
	// Prefabs
	public Transform PoisionessGrapes;
	public Transform Berries;
	public Transform PoisionessBerries;
	public Transform Grapes;
	
	// Static variables
	public static float minYSpawnHeight = 5.0F;
	
	// Use this for initialization
	void Start () {
		this.numSpawned = 0;
		
		// Find the position of the camera.
		this.cameraPosition = Camera.mainCamera.gameObject.transform.position;
		this.terrain 		= Terrain.activeTerrain;
		
		// Spawn a certain number of harvestable objects.
		for(int i = 0; i < iNumDefaultHarvestableObjects; i++){
			spawnHarvestableObject();
		}
	}
	
	// Update is called once per frame
	void Update () {
		numSpawned = transform.childCount;
	}
	
	private void spawnHarvestableObject(){
		// Update camera position
		cameraPosition = Camera.mainCamera.gameObject.transform.position;
		
		// Figure out a random spot to spawn a harvestable object near the camera.
		Vector3 spawnLocation;
		spawnLocation.x = Random.Range(cameraPosition.x - fMaxSpawnDist, cameraPosition.x + fMaxSpawnDist);
		spawnLocation.z = Random.Range(cameraPosition.z - fMaxSpawnDist, cameraPosition.z + fMaxSpawnDist);
		spawnLocation.y = 1.0F;
		
		// Find the Y height of the terrain at this XZ position.
		spawnLocation.y = terrain.SampleHeight(spawnLocation);
		
		// Check to see if this position collides with anything in the terrain.
		// Don't care ATM.
		
		// Place a random harvestable at correct location
		Transform clone = null;
		
		EHarvestType foodType = (EHarvestType)Random.Range(0,3);
		
		
		switch(foodType){
			case EHarvestType.EBerries:
					clone = Instantiate(Berries, spawnLocation, Quaternion.identity) as Transform;
					break;
			
			case EHarvestType.EGrapes:
					clone = Instantiate(Grapes, spawnLocation, Quaternion.identity) as Transform;
					break;
			
			case EHarvestType.EPoisionessGrapes:
					clone = Instantiate(PoisionessGrapes, spawnLocation, Quaternion.identity) as Transform;	
					break;
			
			case EHarvestType.EPoisionessBerries:
					clone = Instantiate(PoisionessBerries, spawnLocation, Quaternion.identity) as Transform;
					break;
			
			default:
					Debug.Log("FactorySpawner: Invalid enum prefab selection 'spawnHarvestableObject()'");
					break;
		}
		
		// Setup the objects script to have a certain type.
		clone.gameObject.AddComponent<Harvest>();
		clone.gameObject.GetComponent<Harvest>().setHarvestType(foodType);
		
		if(clone != null){
			numSpawned++;
			clone.transform.parent = transform;
			clone.transform.name = "harvestable" + numSpawned;
		}
	}
	
	void OnGUI () {
		GUI.Label (new Rect (20,400,400,80), "Number Harvestables Spawned#: " + iNumDefaultHarvestableObjects);
	}
}
