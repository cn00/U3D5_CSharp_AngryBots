using UnityEngine;
using System.Collections;
using VoiceChannel;
using SimpleJSON;

public class TestVoiceChannel : MonoBehaviour {

//	private bool isOnline = false;
//	private string status = "";
	public bool isShowVoiceUI = false;
	string[] channelInfo = {"eyJjaGFubmVsSWQiOiIyIiwicm9vbUlkIjo3OTU5NSwic2VydmVySWQiOjcxODUsInR5cGUiOjF9","eyJjaGFubmVsSWQiOiIzIiwicm9vbUlkIjo3OTU5Niwic2VydmVySWQiOjcxODUsInR5cGUiOjF9"};
	private int index = 0;
	private string m_InGameLog = "";
	private Vector2 m_Position = Vector2.zero;

	void showStatus(string msg) {
//		status = GUI.TextArea(new Rect(40,300,400,400), status + msg + "\n");
//		ToastBuilder.CreateMovingToast(msg, 1.0f);
		m_InGameLog = msg + "\n" + m_InGameLog;
	}

	// Use this for initialization
	void Start () {
		VoiceChannelPlugin.SetDebugLog(true);
		VoiceChannelPlugin.Init("c3ae9769-de6b-454c-9a41-9c724250923c");

		string userid = SystemInfo.deviceUniqueIdentifier;
		VoiceChannelPlugin.SetLoginInfo(userid, userid, null);

		Debug.Log("current object: " + this.gameObject.name);
		VoiceChannelPlugin.AddCallback(this.gameObject.name);
	}

	void OnGUI() {
		if (!isShowVoiceUI)
			return;
		int inset = Screen.width / 20;
		int space = Screen.width / 30;
		int btnsOneRow = 3;
		int btnWidth = (Screen.width - inset * 2 - space * (btnsOneRow - 1)) / btnsOneRow;
		int btnHeight = btnWidth / 3;

		int labelX = inset;
		int labelY = inset + (btnHeight + space) * 5;
		int labelWidth = Screen.width - labelX * 2;
		int labelHeight = Screen.height - labelY;

		GUI.BeginGroup(new Rect(labelX, labelY, labelWidth, labelHeight));
		
		m_Position = GUILayout.BeginScrollView (m_Position, GUILayout.Width(labelWidth), GUILayout.Height(labelHeight));
		GUILayout.Label(m_InGameLog);
		GUILayout.EndScrollView();
		
		GUI.EndGroup();

		if(GUI.Button(new Rect(inset, inset, btnWidth, btnHeight), "JoinChannel")){
			string currentChannel = channelInfo[(index++ % channelInfo.Length)];

			Debug.Log("JoinChannel: " + currentChannel);
			showStatus("JoinChannel: " + currentChannel);

			VoiceChannelPlugin.JoinChannel(currentChannel, null);
			VoiceChannelPlugin.RequestChannelDetail(currentChannel);
		}
		
		if(GUI.Button(new Rect(inset + btnWidth + space, inset, btnWidth, btnHeight), "ExitChannel")){
			Debug.Log("ExitChannel");
			showStatus("ExitChannel");

			VoiceChannelPlugin.ExitChannel();
		}

		if(GUI.Button(new Rect(inset + (btnWidth + space) * 2, inset, btnWidth, btnHeight), "Exit")){
			showStatus("Exit");
			VoiceChannelPlugin.Exit();
		}
		
		if(GUI.Button(new Rect(inset, inset + btnHeight + space, btnWidth, btnHeight), "StartTalk")){
			Debug.Log("StartTalk");
			showStatus("StartTalk");

			VoiceChannelPlugin.StartTalking();
		}
		
		if(GUI.Button(new Rect(inset + btnWidth + space, inset + btnHeight + space, btnWidth, btnHeight), "StopTalk")){
			Debug.Log("StopTalk");
			showStatus("StopTalk");

			showStatus("send bytes: " + VoiceChannelPlugin.GetCurrentVoiceTrafficSend());
			VoiceChannelPlugin.StopTalking();
		}
		
		if(GUI.Button(new Rect(inset, inset + (btnHeight + space) * 2, btnWidth, btnHeight), "Mute")){
			Debug.Log("Mute");
			showStatus("Mute");

			VoiceChannelPlugin.Mute();
		}
		
		if(GUI.Button(new Rect(inset + btnWidth + space, inset + (btnHeight + space) * 2, btnWidth, btnHeight), "Restore")){
			Debug.Log("Restore");
			showStatus("Restore");

			VoiceChannelPlugin.Restore();
		}
		
		if(GUI.Button(new Rect(inset, inset + (btnHeight + space) * 3, btnWidth, btnHeight), "SetFree")){
			Debug.Log("SetFree");
			showStatus("SetFree");

			VoiceChannelPlugin.SetChannelTalkMode(TalkMode.Freedom);
		}
		
		if(GUI.Button(new Rect(inset + btnWidth + space, inset + (btnHeight + space) * 3, btnWidth, btnHeight), "SetAdminOnly")){
			Debug.Log("SetAdminOnly");
			showStatus("SetAdminOnly");

			VoiceChannelPlugin.SetChannelTalkMode(TalkMode.AdministratorOnly);
		}

		if(GUI.Button(new Rect(inset, inset + (btnHeight + space) * 4, btnWidth, btnHeight), "Clear")){
			m_InGameLog = "";
		}
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void onExit(string param) {
		Debug.Log("onExit:\n" + param);
		showStatus("onExit: " + param);
	}
	
	public void onJoinChannel(string param) {
		Debug.Log("onJoinChannel:\n" + param);
		showStatus("onJoinChannel: " + param);
	}
	
	public void onGetChannelMember(string param) {
		Debug.Log("onGetChannelMember:\n" + param);
		showStatus("onGetChannelMember: " + param);

		var obj = JSONNode.Parse(param);
		string userId = obj["userId"];
		VoiceChannelPlugin.RequestUserNickname(new string[]{userId}, 1);
	}

	public void onGetUserNickname(string param) {
		Debug.Log("onGetUserNickname:\n" + param);
		showStatus("onGetUserNickname: " + param);
	}

	public void onExitChannel(string param) {
		Debug.Log("onExitChannel:\n" + param);
		showStatus("onExitChannel: " + param);
	}

	public void onChannelRemoved(string param) {
		Debug.Log("onChannelRemoved:\n" + param);
		showStatus ("onChannelRemoved: " + param);
	}

	public void onRemoveChannelMember(string param) {
		Debug.Log("onRemoveChannelMember:\n" + param);
		showStatus("onRemoveChannelMember: " + param);
	}
	
	public void onSilencedStateChanged(string param) {
		Debug.Log("onSilencedStateChanged:\n" + param);
		showStatus("onSilencedStateChanged: " + param);
	}
	
	public void onMuteStateChanged(string param) {
		Debug.Log("onMuteStateChanged:\n" + param);
		showStatus("onMuteStateChanged: " + param);
	}
	
	public void onStartTalking(string param) {
		Debug.Log("onStartTalking:\n" + param);
		showStatus("onStartTalking: " + param);
	}
	
	public void onStopTalking(string param) {
		Debug.Log("onStopTalking:\n" + param);
		showStatus("onStopTalking: " + param);
	}
	
	public void notifyChannelMemberTypes(string param) {
		Debug.Log("notifyChannelMemberTypes:\n" + param);
		showStatus("notifyChannelMemberTypes: " + param);
	}
	
	public void notifyChannelTalkMode(string param) {
		Debug.Log("notifyChannelTalkMode:\n" + param);
		showStatus("notifyChannelTalkMode: " + param);
	}

	public void onGetChannelDetail(string param) {
		Debug.Log ("onGetChannelDetail:\n" + param);
		showStatus("onGetChannelDetail: " + param);
	}

	public void onError(string param) {
		Debug.Log("onError:\n" + param);
		showStatus("onError: " + param);
	}
}
