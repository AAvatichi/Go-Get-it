using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {
    public float speed = 0.5f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = transform.position + new Vector3(0, 0, 0.5f) * Time.deltaTime;
        transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime);
    }
}
