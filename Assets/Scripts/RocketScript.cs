using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {

    public Transform explosion;
    public int rotationSpeed = 60;
    public int speed = 6000;

    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(-transform.up * speed);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        explosion = Instantiate(explosion.transform);
        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.transform.rotation = transform.rotation;
            //Destroy((explosion2 as Transform).gameObject, 0.5f);
        }

        Destroy(gameObject);
        Destroy((explosion as Transform).gameObject, 0.5f);
    }
}
