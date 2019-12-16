using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public UILabel scoreLabel = null;
	ScoreManager scoreManager = null;

	void Start(){
		scoreManager = GameObject.Find ("ScoreManager").GetComponent<ScoreManager>();

		if (scoreManager && scoreLabel) {
			scoreLabel.text = "Final Score: " + scoreManager.GetScore ();
		}
	}

	void RestartGame()
	{
		if (scoreManager) {
			Destroy (scoreManager);
		}
		Application.LoadLevel("LoadMenu");
	}
}
