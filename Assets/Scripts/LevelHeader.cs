using UnityEngine;
using System.Collections;

public class LevelHeader : UILabel {

	ScoreManager scoreManager = null;
	// Use this for initialization
	void Start () {
		scoreManager = GameObject.Find ("ScoreManager").GetComponent<ScoreManager>();
		if (scoreManager) {
			text = "Level " + scoreManager.GetLevel();
		}
	}
}
