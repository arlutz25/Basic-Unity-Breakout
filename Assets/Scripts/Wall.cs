using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	
	// Use this for initialization
	void Start () {
		//rigidbody.AddForce (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter( Collision col ) {
		
		foreach (ContactPoint contact in col.contacts) {
			if (contact.thisCollider == collider ) {
				
				if (contact.otherCollider.rigidbody)
				{
					AudioSource.PlayClipAtPoint( audio.clip, Vector3.zero );
				}
			}
		}
	}

}