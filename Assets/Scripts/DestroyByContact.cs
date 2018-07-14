using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	
	private GameController gameController;
	private int lives;
	private Transform cam;
	private Vector3 originalPosition;
	
	private float cameraShake = 0.2f;
	
	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			// shouldn't reach this point
			Debug.Log("Cannot find 'GameController' script");
		} else {
			lives = gameController.lives;
		}
		
		cam = Camera.main.transform;
		originalPosition = cam.position;
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) {
			return;
		}
		
		// hazard explosion
		if (explosion != null) {
			Instantiate(explosion, transform.position, transform.rotation);
		}
		
		if (other.CompareTag("Player")) {
			// player explosion
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			lives -= 1;
			if (lives < 1) {
				lives = 0;
			}
			if (lives == 0) {
				// no more lives left, game over
				gameController.UpdateLives(lives);
				gameController.GameOver();
			} else {
				// at least one more life left, update UI
				gameController.UpdateLives(lives);
				gameController.LostALife();
			}
		} else {
			gameController.AddScore(scoreValue);
		}
		
		if (other.gameObject.CompareTag("Player") && lives > 0) {
			other.gameObject.SetActive(false);
		} else {
			Destroy(other.gameObject);
		}
		
		Renderer rend;
		if (gameObject.GetComponent<Renderer>() != null) {
			rend = gameObject.GetComponent<Renderer>();
		} else {
			rend = gameObject.GetComponentInChildren<Renderer>();
		}
		rend.enabled = false;
		
		StartCoroutine(CameraShake(cameraShake));
	}
	
	public IEnumerator CameraShake(float cameraShake) {
		int numShake = 3;
		int sign = -1;
		
		// camera shakes
		for (int i = 0; i < numShake; i++) {
			cam.position = new Vector3(originalPosition.x + (sign * cameraShake), originalPosition.y, originalPosition.z);
			yield return null;
			sign *= -1;
		}
		
		// return camera to original position
		cam.position = originalPosition;
		
		// game object is destroyed
		Destroy(gameObject);
	}
	
}
