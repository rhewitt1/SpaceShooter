using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour {

	public float dodge;
	public float smoothing;
	public float tilt;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	public Boundary boundary;
	
	private float currentSpeed;
	private float targetManeuver;
	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
		currentSpeed = rb.velocity.z;
		StartCoroutine(Evade());
	}

	// returns IEnumerator because this is a coroutine
	IEnumerator Evade() {
		yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
		
		// this is the loop that moves the enemy through the dodging maneuvers
		while (true) {
			// multiplying by -Mathf.Sign of the x-position of the ship moves it to the other side of the x axis
			targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
		}
	}
	
	void FixedUpdate() {
		currentSpeed = rb.velocity.z;
		float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
		// need to make sure that the enemy ship can't dodge off the screen
		rb.position = new Vector3 (Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
		// adding tilt to the enemy ship
		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
	
}
