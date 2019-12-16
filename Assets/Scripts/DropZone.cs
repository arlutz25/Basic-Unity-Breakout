using UnityEngine;
using System.Collections;

public class DropZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Check for trigger
	void OnTriggerEnter( Collider other ){
		Ball gameBall = other.GetComponent<Ball>();

		if (gameBall) {
			AudioSource.PlayClipAtPoint( audio.clip, Vector3.zero );
			gameBall.Die();
		}
	}
}
