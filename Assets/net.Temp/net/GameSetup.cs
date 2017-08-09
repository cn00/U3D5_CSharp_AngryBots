using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour {
	
	public Transform playerPref;
	public string gameName = "Example4";
	public FPSChat chatScript;
	public string playerName = "";

	//Server-only playerlist
	public ArrayList playerList = new ArrayList();
	class FPSPlayerNode {
		public string playerName;
		public NetworkPlayer networkPlayer;
		
		public int kills =0;
		public int deaths =0;	
	}
	
	
	void Awake() 
	{
		playerName = PlayerPrefs.GetString("playerName");
		
		chatScript = GetComponent<FPSChat>();
		Network.isMessageQueueRunning = true;
//		Screen.lockCursor=true;	
	}

	void Start(){
		
		if(Network.isServer || Network.isClient){
			
			chatScript.ShowChatWindow();
			GetComponent<NetworkView>().RPC ("TellOurName", RPCMode.AllBuffered, playerName);
			
			foreach (GameObject go in FindObjectsOfType<GameObject>()){
				go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);	
			}	
			
			
			
		}else{
			//How did we even get here without connection?
			Screen.lockCursor=false;	
			Application.LoadLevel((Application.loadedLevel-1));		
		}

	}
	
	//Server function
	void OnPlayerDisconnected(NetworkPlayer player) {
		Network.RemoveRPCs(player, 0);
		Network.DestroyPlayerObjects(player);
		
		//Remove player from the server list
		foreach(FPSPlayerNode entry in  playerList){
			if(entry.networkPlayer==player){
				chatScript.addGameChatMessage(entry.playerName+" disconnected from: " + player.ipAddress+":" + player.port);
				playerList.Remove(entry);
				break;
			}
		}
	}
	
	//Server function
	void OnPlayerConnected(NetworkPlayer player) {
		chatScript.addGameChatMessage("Player connected from: " + player.ipAddress +":" + player.port);
//		foreach(NetworkPlayer nplayer in playerList)
//		{
//			GetComponent<NetworkView>().RPC("TellOurName", RPCMode.Others, player);
//		}
	}
	

	//Sent by newly connected clients, recieved by server
	[RPC] void TellOurName(string name, NetworkMessageInfo info){
		NetworkPlayer netPlayer = info.sender;
		if(netPlayer+""=="-1"){
			//This hack is required to fix the local players networkplayer when the RPC is sent to itself.
			netPlayer=Network.player;
		}
		
		FPSPlayerNode newEntry = new FPSPlayerNode();
		newEntry.playerName=name;
		newEntry.networkPlayer=netPlayer;
		playerList.Add(newEntry);
		
		if(Network.isServer){
			chatScript.addGameChatMessage(name+" joined the game");
		}
	}
	
	//Called via Awake()
	void OnNetworkLoadedLevel()
	{
		// Randomize starting location
		GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag ("SpawnPoint");
		
		int n = Random.Range(0, spawnpoints.Length);
		Transform spawnpoint = spawnpoints[n].transform;
		Transform newTrans = (Transform)Network.Instantiate(playerPref,/*spawnpoint.position+*/new Vector3(-5.18f,2.5f,13.0f), spawnpoint.rotation, 0);
//		Camera.main.transform.position = newTrans.position+new Vector3(0,15.5f,0);
		if (Network.isServer) {
			Debug.Log ("spawns: " + n + "/" + spawnpoints.Length);
		}
	}
	
	
	void OnDisconnectedFromServer () {
		//Load main menu
		Screen.lockCursor=false;
		Application.LoadLevel((Application.loadedLevel-1));
	}
}
