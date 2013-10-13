using UnityEngine;
using System.Collections;

public class CameraRayCasting : MonoBehaviour {

    public RaycastHit hit;
    public static Collider collider1 = new Collider();
    private Ray ray;
    private Vector3 vec;
    private LayerMask layerMask;

    // Use this for initialization
    void Start () {
   
    }
  
    // Update is called once per frame
    void Update () {
      
        // Find the centre of the Screen
        vec.x = (float)Screen.width / 2;
        vec.y = (float)Screen.height / 2;
        vec.z = 0;
      
        // Create the actual Ray based on the screen vector above
        ray = camera.ScreenPointToRay(vec);

        // Only look in Layer 8 (Level Assets)
        // you can remove this line if you want the ray to hit everything or change the 8
        // to whatever layer you need. This is a good feature as if you know all the objects
        // you need to possibly hit are in one layer, why check for everything else.
        layerMask = 1 << 8;

        // This returns true if an object is hit by the ray
        if (Physics.Raycast(ray, out hit, 3.0f, layerMask)) //remove layerMask if you remove it in line above
        {
            //stores the object hit
            collider1 = hit.collider;

            // Debug information - this can be deleted.
            // Draws a line to show the actual ray.
            // Outputs the name of the object hit
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.Log(collider1.name); // this will tell you what you are hitting
        }
    }
}
