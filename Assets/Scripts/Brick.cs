using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public int pointValue = 1;
	public int hitPoints = 1;
	public int row = 0;

	// Use this for initialization
	void Start () {
		Paddle gamePaddle = GameObject.Find ("Paddle").GetComponent<Paddle> ();
		
		gamePaddle.AddBrick (row);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter( Collision col ) {

		foreach (ContactPoint contact in col.contacts) {
			if (contact.thisCollider == collider ) {
				//This is paddle's contact point
				float english = contact.point.x -transform.position.x;
				float difference = contact.point.y -transform.position.y;

				float forceX = contact.otherCollider.rigidbody.velocity.x;
				float forceY = contact.otherCollider.rigidbody.velocity.y;


				if ( Mathf.Abs(difference) > .5f)
				{
					forceY *= -1f;
				}
				if ( Mathf.Abs(english) > 1f)
				{
					forceX *= -1f;
				}

				if (contact.otherCollider.rigidbody)
				{
					AudioSource.PlayClipAtPoint( audio.clip, Vector3.zero );
					contact.otherCollider.rigidbody.velocity = new Vector3( forceX, forceY, 0f );
				}
			}
		}

		// Brick takes a hit
		hitPoints--;
		renderer.material.color = Color.grey;
		if (hitPoints <= 0) {
			Die();
		}
	}

	public void Vanish()
	{
		Destroy (gameObject);
	}

	void Die() {
		Vanish ();

		Paddle gamePaddle = GameObject.Find ("Paddle").GetComponent<Paddle> ();

		gamePaddle.AddPoint(pointValue, row);

	}
}
