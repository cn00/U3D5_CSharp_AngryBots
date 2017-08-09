using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public string thisName = "Bugged name";
	public NetworkView rigidBodyView;
	public Health health;
//	public scoreBoard theScoreBoard;
	public bool isLocalPlayer = false;

	public Material metalMaterial;
	public Material orgMaterial;
	
	public MovementMotor motor;

	private float coloredUntill;
	private bool invincible;
	
	
	void Awake(){
//		orgMaterial = GetComponent<Renderer>().material;
		Debug.Log (thisName);
//		theScoreBoard= GameObject.Find("Generalscripts").GetComponent(scoreBoard);
	}
	
	void OnNetworkInstantiate (NetworkMessageInfo msg) {
		// This is our own player
		if (GetComponent<NetworkView>().isMine)
		{
			//camera.main.enabled=false;
				
			isLocalPlayer=true;
			GetComponent<NetworkView>().RPC("setName", RPCMode.Others, thisName);
			
			Destroy(GameObject.Find("LevelCamera"));
			thisName=PlayerPrefs.GetString("playerName");
			
//			Machinegun gun = transform.Find("CrateCamera/Weapon").GetComponent("Machinegun");
//			gun.isLocalPlayer=true;	
		}
		// This is just some remote controlled player, don't execute direct
		// user input on this. DO enable multiplayer controll
		else
		{
			thisName="Remote"+Random.Range(1,10);
			name += thisName;
			
//			transform.Find("CrateCamera").gameObject.active=false;			
//			FPSWalker tmp2 = GetComponent(FPSWalker);
//			tmp2.enabled = false;
//			MouseLook tmp5 = GetComponent(MouseLook);
//			tmp5.enabled = false;
			NetworkView owner = GetComponent<NetworkView>();
			GetComponent<NetworkView>().RPC("askName", owner.viewID.owner, Network.player);
					
		}
	}
	
	
	
	void OnGUI(){
		if(isLocalPlayer){
			GUILayout.Label("HP: "+health.health);
		}
	}
	
	
	[RPC]
	IEnumerator StartInvincibility(){
		invincible=true;
		GetComponent<Renderer>().material=metalMaterial;
		
		yield return new WaitForSeconds(10.0f);

		GetComponent<Renderer>().material=orgMaterial;	
		invincible=false;
	}
	
	
	void ApplyDamage (string[] info ){
		float damage = float.Parse(info[0]);
		string killerName = info[1];
		
		if(invincible){
			return;
		}
		
		health.SetHealth(health.health - (int)damage);
		if(health.health<0){
//			theScoreBoard.LocalPlayerHasKilled();
			GetComponent<NetworkView>().RPC("Respawn",RPCMode.All);
		}else{
			GetComponent<NetworkView>().RPC("setHP",RPCMode.Others, health.health); 
		}
	}
	
	
	[RPC]
	void setHP(int newHP){
		health.SetHealth(newHP);
	}
	
	
	
	[RPC]
	void Respawn(){
		if (GetComponent<NetworkView>().isMine)
		{
//			theScoreBoard.LocalPlayerDied();
			
			// Randomize starting location
			GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag ("Spawnpoint");
			Transform spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Length)].transform;
			
			transform.position=spawnpoint.position;
			transform.rotation=spawnpoint.rotation;	
		}
		health.SetHealth(100.0f);
	}
	
	
	
	[RPC]
	void setName(string name){
		thisName=name;
	}
	
	[RPC]
	void askName(NetworkPlayer asker){
		GetComponent<NetworkView>().RPC("setName", asker, thisName);
	}
}
