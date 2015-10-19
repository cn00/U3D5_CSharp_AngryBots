using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButtonClick : MonoBehaviour {
	static int i = 0;
	public Health health;
	public TestVoiceChannel testVoice;

	public void OnAddHealth10()
	{
		++i;
		foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
			if (player.GetComponent<PlayerScript> ().isLocalPlayer) {
				health = player.GetComponent<Health>();
				break;
			}
		}
		if (health) {
			Debug.Log ("Add Health clicked: " + i);
			Text text = GetComponentInChildren<Text> ();
			text.text = ("Add Health:" + i);
			health.SetHealth (health.health + 10);
		}
	}

	public void OnShowVoiceUI(){
		testVoice.isShowVoiceUI = !testVoice.isShowVoiceUI;
	}

	public void OnLogout(){
		Network.Disconnect(200);
	}
}
