using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;

	void Start() {
		// the laser bolt will always be moving forward along the z axis
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
	
	public void UpdateSpeed(float addToSpeed) {
		// this should speed up the hazard velocity
		GetComponent<Rigidbody>().AddForce(transform.forward * (-addToSpeed), ForceMode.VelocityChange);
	}
	
}
