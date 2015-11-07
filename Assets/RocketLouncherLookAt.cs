using UnityEngine;
using System.Collections;

public class RocketLouncherLookAt : MonoBehaviour {

    public Transform rocket;
    Vector3 hitPoint = new Vector3();

    public float timeBetweenBullets = 1f;        // The time between each shot.

    float timer;                                    // A timer to determine when to fire.

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        findHitPoint(out hitPoint);
        transform.forward = hitPoint - transform.position;

        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the Fire1 button is being press and it's time to fire...
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            // ... shoot the gun.
            Shoot();
        }
    }

    Collider findHitPoint(out Vector3 hitPoint)
    {
        /*
            The function find where to shoot at, and if the shot hit's a collider,
            the function puts the end point or hit point in the variable "hitPoint".
            If the line hits some thing then the function will return the collider
            of the object it hited. Other wise it will return null.
        */

        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray camRay = Camera.main.ScreenPointToRay(screenCenter);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit hit;
        float camRayLength = 100;
        Collider collider = null;

        // Perform the raycast and if it hits something    
        if (Physics.Raycast(camRay, out hit, camRayLength))
        {
            collider = hit.collider;
            hitPoint = hit.point;
        }
        else
        {
            //didnt hit, so calculate the ending of the line
            Vector3 startPoint = Camera.main.ScreenToWorldPoint(screenCenter);//Vector3(xHitPoint, yHitPoint, Camera.main.nearClipPlane));
            Vector3 direction = Camera.main.transform.forward;
            //make sure the length of the vector is 1:
            direction.Normalize();
            hitPoint = startPoint + (direction * 100);
        }


        return collider;

    }

    void Shoot()
    {
        // Reset the timer.
        timer = 0f;
        Transform rocket2 = Instantiate(rocket.transform);
        if (rocket2 != null)
        {
            rocket2.transform.position = transform.position;
            rocket2.transform.forward = hitPoint - transform.position;
            rocket2.transform.Rotate(-90, 0, 0);
            Destroy((rocket2 as Transform).gameObject, 3f);
        }
    }
}
