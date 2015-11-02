using UnityEngine;
using System.Collections;

public class GunAiming : MonoBehaviour {

    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    // Update is called once per frame
    void Update () {
        Turn();
    }

    void Turn()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray camRay = Camera.main.ScreenPointToRay(screenCenter);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit hit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out hit, camRayLength))
        {

            //transform.LookAt(hit.point);
            
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = transform.position - hit.point;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            transform.rotation = newRotation;
            //gunRigidbody.MoveRotation(newRotation);
            
        }
    }
}
