using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyButton : MonoBehaviour {
	public Health health;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	static int i = 0;
	public void OnClick()
	{
		++i;
		Debug.Log ("Add Health clicked: "+i);
		Text text = GetComponentInChildren<Text> ();
		text.text = ( "Add Health:" + i);
		health.SetHealth( health.health + 10);
	}
}
