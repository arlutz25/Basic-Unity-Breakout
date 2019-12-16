using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public float paddleSpeed = 15f;
	public GameObject BallPrefab;
	public GameObject BrickPrefab;
	public UIPanel playPanel;
	public UILabel levelHeader;

	GameObject attachedBall = null;
	GameObject gameBall = null;
	//GameObject playPanel = null;

	ScoreManager scoreManager;

	int[] leftInRow = new int[8];
	
	//GUIText guiLives;
	//public GUISkin scoreboardSkin;

	public UILabel scoreLabel;
	public UILabel livesLabel;
	public UILabel levelLabel;

	int bricksLeft = 0;
	
	float speed = 1f;

	// Use this for initialization
	void Start () {
		speed = 1.0f;

		scoreManager = GameObject.Find ("ScoreManager").GetComponent<ScoreManager>();
		if (!scoreManager) {
			scoreManager = new ScoreManager();
		}

		//Spawn a ball
		GameObject ballObject = GameObject.Find ("Ball");
		if (!ballObject) {
			gameBall = null;
			SpawnBall();
		}
		
		transform.localScale = new Vector3(6.0f, 0.5f, 1.0f);

	}

	public void AddBrick(int row)
	{
		bricksLeft++;
		leftInRow [row]++;
	}

	public void LoseLife() {
		// Decrease lives
		scoreManager.LoseLife ();
		// Spawn a new ball if you have a life left
		if (scoreManager.GetLives() > 0) {
			playPanel.enabled = true;
			levelHeader.enabled = true;
			playPanel.alpha = 1f;
			SpawnBall ();
		} 

	}

	public void AddPoint(int v, int row) {
		scoreManager.AddScore (v);
		//scoreLabel.text = "Score: " + score;
		leftInRow [row]--;
		bricksLeft--;

		if (row == 7) {
			transform.localScale = new Vector3(3.0f, 0.5f, 1.0f);
		}

		if (leftInRow [row] == 0) {
			// Speed up!

			//GameObject ballObject2 = GameObject.Find ("Ball");
			if (gameBall) {
				Debug.Log("Speed Up!");
				float velX = gameBall.rigidbody.velocity.x / speed;
				float velY = gameBall.rigidbody.velocity.y / speed;

				speed += 0.5f;

				gameBall.rigidbody.velocity = new Vector3(velX * speed, velY * speed, 0f);
			}


		}

		if (bricksLeft == 0) {
			scoreManager.NextLevel();
			Application.LoadLevel("Level1");
		}
	}

	public void SpawnBall() {
		if (BallPrefab == null) {
			Debug.Log( "Hey you!  Forget something?" );
			return;
		}

		// Prepare to launch ball
		Vector3 ballPosition = transform.position + new Vector3(0, 0.75f, 0);
		Quaternion ballRotation = Quaternion.identity;
		attachedBall = (GameObject)Instantiate (BallPrefab, ballPosition, ballRotation);
		//playPanel = (GameObject)Instantiate(PanelPrefab, new Vector3(0f, 0f, -145f), Quaternion.identity);

	}

//	void OnGUI() {
//		// Scoreboard
//		GUI.skin = scoreboardSkin;
//		GUI.Label (new Rect (0, 10, 100, 100), "Score: " + score);
//	}
	
	// Update is called once per frame
	void Update () {


		if (livesLabel) {
			livesLabel.text = "Lives: " + scoreManager.GetLives ();
		}
		if (scoreLabel) {
			scoreLabel.text = "Score: " + scoreManager.GetScore ();
		}
		if (levelLabel) {
			levelLabel.text = "Level: " + scoreManager.GetLevel ();
		}

		// Move paddle
		float paddleLentgh = transform.localScale.x / 2f;
		transform.Translate( paddleSpeed * Time.deltaTime * Input.GetAxis( "Horizontal" ), 0, 0 );
		float boundary = 14f - paddleLentgh;

		if (transform.position.x > boundary) {
			transform.position = new Vector3( boundary, transform.position.y, transform.position.z );
		}
		if (transform.position.x < -boundary) {
			transform.position = new Vector3( -boundary, transform.position.y, transform.position.z );
		}

		// Launch Ball
		if (attachedBall) {
			Rigidbody ballRigidBody = attachedBall.rigidbody;

			ballRigidBody.position = transform.position + new Vector3 (0, 0.75f, 0);

			if (Input.GetButtonDown ("Jump")) {
					//Destroy (playPanel);
					ballRigidBody.isKinematic = false;
					ballRigidBody.AddForce (0f, 300f * speed, 0);
					gameBall = attachedBall;
					attachedBall = null;
			}
		}

	}
	

	void LaunchBall(){
		if (attachedBall) {
			playPanel.enabled = false;
			levelHeader.enabled = false;

			Rigidbody ballRigidBody = attachedBall.rigidbody;

			//Destroy (playPanel);
			ballRigidBody.isKinematic = false;
			ballRigidBody.AddForce (0f, 300f * speed, 0);
			gameBall = attachedBall;
			attachedBall = null;
		}
	}

	void lateUpdate(){
	}

	void OnCollisionEnter( Collision col ) {
		foreach (ContactPoint contact in col.contacts) {
			if (contact.thisCollider == collider ) {
				//This is paddle's contact point
				float english = contact.point.x -transform.position.x;
				float slope = ( -transform.localScale.x / 27f) + (1f / 3f);
				float angle = -english * (Mathf.PI * slope) + (Mathf.PI / 2);


				float ballVel = contact.otherCollider.rigidbody.velocity.magnitude;

				float velX = ballVel * Mathf.Cos(angle);
				float velY = ballVel * Mathf.Sin(angle);


				Ball ballObject3 = contact.otherCollider.GetComponent<Ball>();
				if (ballObject3) {
					AudioSource.PlayClipAtPoint( audio.clip, Vector3.zero );
					Debug.Log( angle * (180 / Mathf.PI) );
					ballObject3.adjustVelocity( velX, velY);
				}


			}
		}
	}
}
