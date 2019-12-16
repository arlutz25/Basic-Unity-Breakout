using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody.AddForce (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Die() {
		//Destroy the ball
		Destroy (gameObject);
		GameObject paddleObject = GameObject.Find ("Paddle");
		Paddle gamePaddle = paddleObject.GetComponent<Paddle> ();

		if (gamePaddle) {
			// Lose a life
			gamePaddle.LoseLife();
		}
	}

	public void adjustVelocity(float velX, float velY)
	{
		rigidbody.velocity = new Vector3( velX, velY, 0f);
	}
}
