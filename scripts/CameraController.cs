using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform[] cameraTargets;
	int cameraIndex;
	
	CameraSettings cameraSettings;
	
	float cameraRotX = 0f;
	
	// Use this for initialization
	void Start ()
	{
		cameraSettings = cameraTargets[cameraIndex].GetComponent<CameraSettings>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Tab)) {
			cameraIndex++;
			if (cameraIndex >= cameraTargets.Length) {
				cameraIndex = 0;
			}
			cameraSettings = cameraTargets[cameraIndex].GetComponent<CameraSettings>();
		}
		
		if (cameraTargets[cameraIndex]) {
			if (cameraSettings.smoothing == 0f) {
				transform.position = cameraTargets[cameraIndex].position;
				transform.rotation = cameraTargets[cameraIndex].rotation;
			}
			else {
				transform.position = Vector3.Lerp(transform.position, cameraTargets[cameraIndex].position, Time.deltaTime * cameraSettings.smoothing);
				transform.rotation = cameraTargets[cameraIndex].rotation;
			}
			cameraRotX -= Input.GetAxis("Mouse Y");
		
			cameraRotX = Mathf.Clamp(cameraRotX, -cameraSettings.cameraPitchMax, cameraSettings.cameraPitchMax);
		
			Camera.main.transform.Rotate(cameraRotX, 0f, 0f);
		}
	}
}
