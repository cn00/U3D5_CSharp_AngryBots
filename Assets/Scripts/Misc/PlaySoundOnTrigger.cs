using UnityEngine;
using System.Collections;

//[RequireComponent (AudioSource)]
public class PlaySoundOnTrigger : MonoBehaviour {
	public bool onlyPlayOnce = true;
	
	private bool playedOnce = false;

	void OnTriggerEnter ( Collider other ) {
		if (playedOnce && onlyPlayOnce)
			return;
		
		GetComponent<AudioSource>().Play ();
		playedOnce = true;
	}
}

