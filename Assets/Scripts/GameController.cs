using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	// Use this for initialization
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text RestartText;
	public Text GameOverText;
	private bool gameOver;
	private bool restart;
	private int Score;

	void Start () {
		gameOver = false;
		restart = false;
		RestartText.text = "";
		GameOverText.text = "";
		Score = 0;
		UpdateScore();
		StartCoroutine (SpawnWaves ());
	}

	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver)
			{
				RestartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}

	}

	public void AddScore (int newScoreValue)
	{
		Score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + Score;
	}

	public void GameOver ()
	{
		GameOverText.text = "Game Over!";
		gameOver = true;
	}
}
