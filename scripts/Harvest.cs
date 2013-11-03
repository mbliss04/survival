using UnityEngine;
using System.Collections;

public class Harvest : MonoBehaviour {
	
	private int harvestableAmount;
	private EHarvestType eFoodType;

	// Use this for initialization
	void Start () {
		harvestableAmount = Random.Range(1,10);
	}
	
	// Update is called once per frame
	void Update () {
		// Nothing
	}
	
	public int getHarvest(){
		return harvestableAmount;
	}
	
	public EHarvestType getHarvestType(){
		return eFoodType;
	}
	
	public void setHarvestType(EHarvestType type){
		eFoodType = type;
	}
	
	public string getName() {
		if (eFoodType == EHarvestType.EBerries || eFoodType == EHarvestType.EPoisionessBerries) {
			return "Berries";
		}
		else {
			return "Grapes";
		}
	}
}