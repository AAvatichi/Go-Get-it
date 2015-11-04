using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    public int damagePerShot = 20;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets = 0.15f;        // The time between each shot.
    public float range = 100f;                      // The distance the gun can fire.

    float timer;                                    // A timer to determine when to fire.
    float shootingInaccuracy = 10f;                 // How accuracy is the shooting. Smaller, means more accuracy
    Ray shootRay;                                   // A ray from the gun end forwards.
    RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
    //int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    ParticleSystem gunParticles;                    // Reference to the particle system.
    LineRenderer gunLine;                           // Reference to the line renderer.
    AudioSource gunAudio;                           // Reference to the audio source.
    Light gunLight;                                 // Reference to the light component.
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.

    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        //shootableMask = LayerMask.GetMask("Shootable");

        // Set up the references.
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the Fire1 button is being press and it's time to fire...
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            // ... shoot the gun.
            Shoot();
        }

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot()
    {
        Debug.Log(timer);
        // Reset the timer.
        timer = 0f;

        // Play the gun shot audioclip.
        gunAudio.Play();

        // Enable the light.
        gunLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position;
        shootHit = findHitPoint();
        shootRay.direction = shootHit.point - transform.position;
        gunLine.SetPosition(1, shootHit.point);

        /*
        shootRay.direction = transform.forward;
        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, range))
        {
            /*
            // Try and find an EnemyHealth script on the gameobject hit.
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            // If the EnemyHealth component exist...
            if (enemyHealth != null)
            {
                // ... the enemy should take damage.
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            * /

        // Set the second position of the line renderer to the point the raycast hit.
        gunLine.SetPosition(1, shootHit.point);
        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
        */
    }

    RaycastHit findHitPoint()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        float xHitPoint = (Screen.width / 2) + Random.Range(-shootingInaccuracy, shootingInaccuracy); // move away from the center of the screen "shootingInaccuracy" pixels
        float yHitPoint = (Screen.height / 2) + Random.Range(-shootingInaccuracy, shootingInaccuracy); // move away from the center of the screen "shootingInaccuracy" pixels

        Vector3 screenCenter = new Vector3(xHitPoint, yHitPoint, 0);
        Ray camRay = Camera.main.ScreenPointToRay(screenCenter);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit hit;
        float camRayLength = 100;
        // Perform the raycast and if it hits something on the floor layer...
        Physics.Raycast(camRay, out hit, camRayLength);
        
        return hit;

    }
}
