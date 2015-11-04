using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
    public Transform blood;
    ParticleSystem hitParticles;
    

    // Use this for initialization
    void Awake () {
        hitParticles = GetComponentInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {

	
	}

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        Transform blood2 = Instantiate(blood.transform);
        if (blood2 != null)
        {
            blood2.transform.position = hitPoint;
            blood2.transform.rotation = transform.rotation;
            Destroy((blood2 as Transform).gameObject, 0.5f);
        }
        /*
        // Set the position of the particle system to where the hit was sustained.
        hitParticles.transform.position = hitPoint;

        // And play the particles.
        hitParticles.Play();
        */

    }
}
