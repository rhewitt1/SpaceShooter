using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject player;

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public int lives;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public GUIText scoreText;
	public GUIText livesText;
	public GUIText gameOverText;
	public GUIText lostALifeText;
	
	private bool gameOver;
	private bool restart;
	private int score;
	private int level;

	// Unity automatically calls this function
	void Start() {
		gameOver = false;
		restart = false;
		score = 0;
		level = 0;
		UpdateScore();
		livesText.text = "Lives: " + lives;
		gameOverText.text = "";
		lostALifeText.text = "";
		StartCoroutine(SpawnWaves());
	}
	
	void Update() {
		if (restart) {
			if (Input.GetKeyDown(KeyCode.R)) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	// spawn the hazards
	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);
		while (true) {
			if (!player.activeSelf) {
				// checks active state of player
				// if there is at least one life left, activate player
				player.SetActive(true);
				player.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
				player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
				
				lostALifeText.text = "";
			}
			
			level += 1;
		
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard;
				if (level < hazards.Length) {
					// no enemy ships for the first couple levels
					hazard = hazards[Random.Range(0, hazards.Length - 1)];
				} else {
					// potentially add ships to the wave
					hazard = hazards[Random.Range(0, hazards.Length)];
				}
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				GameObject newHazardObject = Instantiate(hazard, spawnPosition, spawnRotation);
				
				// speed up hazards each wave
				Mover enemyMover = newHazardObject.GetComponent<Mover>();
				float newSpeed = (float) level/2;
				enemyMover.UpdateSpeed(newSpeed);
					
				yield return new WaitForSeconds(spawnWait);
			}
			
			yield return new WaitForSeconds(waveWait);
			
			if (gameOver) {
				// if there are no lives left and the game is over
				livesText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}
	
	public void AddScore(int newScoreValue) {
		score += newScoreValue;
		UpdateScore();
	}
	
	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}
	
	public void LostALife() {
		lostALifeText.text = "You lost a life!";
	}
	
	public void UpdateLives(int newLives) {
		lives = newLives;
		livesText.text = "Lives: " + lives;
	}
	
	public void GameOver() {
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
	
}
