using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AddHealthButton : MonoBehaviour {
	static int i = 0;
	public Health health;

	public void AddHealth10()
	{
		++i;
		Debug.Log ("Add Health clicked: "+i);
		Text text = GetComponentInChildren<Text> ();
		text.text = ( "Add Health:" + i);
		health.SetHealth( health.health + 10);
	}
}
