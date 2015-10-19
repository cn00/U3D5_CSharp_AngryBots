using UnityEngine;
using System.Collections;

public class VoiceChannelCallback : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * 
	 * 下列所有的函数都是VoiceChannelAPI的回调，只要在需要回调的类中实现相应的函数即可。
	 * 这个类只是为了列出所有的回调，实际应用中可删除。
	 * 
	 */

	public void onExit(string param) {
		Debug.Log("onExit:\n" + param);

	}
	
	public void onJoinChannel(string param) {
		Debug.Log("onJoinChannel:\n" + param);

	}
	
	public void onGetChannelMember(string param) {
		Debug.Log("onGetChannelMember:\n" + param);

	}

	public void onGetUserNickname(string param) {
		Debug.Log("onGetUserNickname:\n" + param);
	}
	
	public void onExitChannel(string param) {
		Debug.Log("onExitChannel:\n" + param);
	}

	public void onChannelRemoved(string param) {
		Debug.Log("onChannelRemoved:\n" + param);
	}

	public void onRemoveChannelMember(string param) {
		Debug.Log("onRemoveChannelMember:\n" + param);

	}
	
	public void onSilencedStateChanged(string param) {
		Debug.Log("onSilencedStateChanged:\n" + param);

	}
	
	public void onMuteStateChanged(string param) {
		Debug.Log("onMuteStateChanged:\n" + param);

	}
	
	public void onStartTalking(string param) {
		Debug.Log("onStartTalking:\n" + param);

	}
	
	public void onStopTalking(string param) {
		Debug.Log("onStopTalking:\n" + param);

	}
	
	public void notifyChannelMemberTypes(string param) {
		Debug.Log("notifyChannelMemberTypes:\n" + param);

	}
	
	public void notifyChannelTalkMode(string param) {
		Debug.Log("notifyChannelTalkMode:\n" + param);

	}

	public void onGetChannelDetail(string param) {
		Debug.Log ("onGetChannelDetail:\n" + param);
	}

	public void onError(string param) {
		Debug.Log("onError:\n" + param);

	}
}
