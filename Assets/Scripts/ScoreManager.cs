using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	int score = 0;
	int level = 1;
	int lives = 3;

	void Start() {
		//Make sure the object persists
		DontDestroyOnLoad (gameObject);
		Application.LoadLevel ("Level1");
	}

	public int GetLevel(){
		return level;
	}

	public void NextLevel(){
		level++;
	}

	public int GetScore(){
		return score;
	}

	public void AddScore( int i) {
		score += i;
	}

	public int GetLives(){
		return lives;
	}

	public void LoseLife()
	{
		lives--;
		if (lives == 0) {
			Application.LoadLevel("GameOver");
		}
	}
}
