using UnityEngine;
using System.Collections;

public class Connect : MonoBehaviour {
	public string connectToIP = "127.0.0.1";
	public int connectPort = 25001;
	public string playerName;
	

	// Use this for initialization
	void Start () {
		connectToIP = PlayerPrefs.GetString ("connectToIP");
		connectPort = int.Parse(PlayerPrefs.GetString ("connectPort"));
		playerName = PlayerPrefs.GetString ("playerName");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void GUIWindow0(int id){
		GUILayout.BeginVertical();
			GUILayout.Space(10);
			GUILayout.Label("Connection status: Disconnected");
			GUILayout.Label("IP:");
			connectToIP = GUILayout.TextField(connectToIP, GUILayout.MinWidth(100));
			GUILayout.Label("Port:");
			connectPort = int.Parse(GUILayout.TextField(connectPort.ToString()));
			GUILayout.Label("NickName:");
			playerName = GUILayout.TextField (playerName);
		GUILayout.EndVertical();

		if (GUI.changed) {
			PlayerPrefs.SetString ("connectToIP", connectToIP);
			PlayerPrefs.SetString ("connectPort", connectPort.ToString());
			PlayerPrefs.SetString ("playerName", playerName);
		}

		GUILayout.BeginVertical();
		if (GUILayout.Button ("Connect as client"))
		{
			//Connect to the "connectToIP" and "connectPort" as entered via the GUI
			//Ignore the NAT for now
			Network.useNat = false;
			Network.Connect(connectToIP, connectPort);
		}
		
		if (GUILayout.Button ("Start Server"))
		{
			//Start a server for 32 clients using the "connectPort" given via the GUI
			//Ignore the nat for now	
			Network.useNat = false;
			Network.InitializeServer(32, connectPort);
		}
		GUILayout.EndVertical();

	}

	void startLevel(int level){
		if(Application.CanStreamedLevelBeLoaded (level)){
			GUILayout.Label("Loading the game: "
			                +Mathf.Floor(Application.GetStreamProgressForLevel(level)*100)+" %");

			Application.LoadLevel(Application.loadedLevel+1);
		}else{
			GUILayout.Label("Loading the game: "
			                +Mathf.Floor(Application.GetStreamProgressForLevel(level)*100)+" %");
		}
	}

	void GUIWindow1(int id)
	{
		
		if (Network.peerType == NetworkPeerType.Connecting) {
			
			GUILayout.Label ("Connection status: Connecting");
			
		} else if (Network.peerType == NetworkPeerType.Client) {
			
			GUILayout.Label ("Connection status: Client!");
			GUILayout.Label ("Ping to server: " + Network.GetAveragePing (Network.connections [0]));		

		} else if (Network.peerType == NetworkPeerType.Server) {
			
			GUILayout.Label ("Connection status: Server!");
			GUILayout.Label ("Connections: " + Network.connections.Length);
			if (Network.connections.Length >= 1) {
				GUILayout.Label ("Ping to first player: " + Network.GetAveragePing (Network.connections [0]));
			}

		} else {
			startLevel(Application.loadedLevel-1);
		}
		
		if (GUILayout.Button ("Disconnect"))
		{
			Network.Disconnect(200);
		}
	}

	//Obviously the GUI is for both client&servers (mixed!)
	void OnGUI ()
	{
		
		if (Network.peerType == NetworkPeerType.Disconnected){
			//We are currently disconnected: Not a client or host
			GUILayout.Window(0, new Rect(Screen.width/4, Screen.height/5,
										 Screen.width/2, Screen.height*3/5),
							 GUIWindow0, "config server");
			
		}else{
			//We've got a connection(s)!
			GUILayout.Window(1, new Rect(Screen.width/4, Screen.height/5,
			                             Screen.width/2, Screen.height*3/5),
			                 GUIWindow1, "server status");

		}

		if (Network.isClient || Network.isServer) {
			//Since we're connected, load the game
			startLevel(Application.loadedLevel+1);
		}

	}
	
	// NONE of the functions below is of any use in this demo, the code below is only used for demonstration.
	// First ensure you understand the code in the OnGUI() void above.
	
	//Client functions called by Unity
	void OnConnectedToServer() {
		Debug.Log("This CLIENT has connected to a server");	
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Debug.Log("This SERVER OR CLIENT has disconnected from a server");
	}
	
	void OnFailedToConnect(NetworkConnectionError error){
		Debug.Log("Could not connect to server: "+ error);
	}
	
	
	//Server functions called by Unity
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player connected from: " + player.ipAddress +":" + player.port);
	}
	
	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
	}
	
	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Player disconnected from: " + player.ipAddress+":" + player.port);
	}
	
	
	// OTHERS:
	// To have a full overview of all network functions called by unity
	// the next four have been added here too, but they can be ignored for now
	
	void OnFailedToConnectToMasterServer(NetworkConnectionError info){
		Debug.Log("Could not connect to master server: "+ info);
	}
	
	void OnNetworkInstantiate (NetworkMessageInfo info) {
		Debug.Log("New object instantiated by " + info.sender);
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		//Custom code here (your code!)
	}
	
	/* 
 The last networking functions that unity calls are the RPC functions.
 As we've added "OnSerializeNetworkView", you can't forget the RPC functions 
 that unity calls..however; those are up to you to implement.
 
 @RPC
 void MyRPCKillMessage(){
	//Looks like I have been killed!
	//Someone send an RPC resulting in this void call
 }
*/

}
