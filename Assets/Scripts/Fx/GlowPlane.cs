using UnityEngine;
using System.Collections;

//2013年7月27日17:46:42，郭志程

public class GlowPlane : MonoBehaviour {

    private Vector3 pos;
    private Vector3 scale;
	public float minGlow = 0.2f;
	public float maxGlow = 0.5f;
	public Color glowColor = Color.white;

    private Material mat;

    void Start (){
//	    if (!getNearstPlayerTransform()) {
//			foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player")){
//				if(player.GetComponent<PlayerScript>().isLocalPlayer){
//					getNearstPlayerTransform() = player.transform;
//					break;
//				}
//			}
//		}
	    pos = transform.position;
	    scale = transform.localScale;
	    mat = GetComponent<Renderer>().material;
	    enabled = false;
    }
	
	Transform getNearstPlayerTransform(){
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");    
		foreach (GameObject player in players) {
			if((player.transform.position - transform.parent.position).magnitude 
			   < (player.transform.position - transform.parent.position).magnitude)
				return player.transform;
		}
		return null;
	}

	void OnDrawGizmos (){	    
        Gizmos.color = new Color(glowColor.r, glowColor.g, glowColor.b, maxGlow * 0.25f);
	    Gizmos.matrix = transform.localToWorldMatrix;
        Vector3 scale = 5.0f * Vector3.Scale(Vector3.one, new Vector3(1, 0, 1));     // Vector3.one表示的是(1,1,1)
	    Gizmos.DrawCube (Vector3.zero, scale);
	    Gizmos.matrix = Matrix4x4.identity;
    }

    void OnDrawGizmosSelected (){	    
        Gizmos.color = new Color(glowColor.r, glowColor.g, glowColor.b, maxGlow);
	    Gizmos.matrix = transform.localToWorldMatrix;
	    Vector3 scale = 5.0f * Vector3.Scale (Vector3.one, new Vector3(1,0,1));
	    Gizmos.DrawCube (Vector3.zero, scale);
	    Gizmos.matrix = Matrix4x4.identity;
    }

    void OnBecameVisible (){
	    enabled = true;	
    }

    void OnBecameInvisible (){
	    enabled = false;
    }

    void Update (){
		if (!getNearstPlayerTransform())
			return;
	    Vector3 vec = (pos - getNearstPlayerTransform().position);
	    vec.y = 0.0f;
	    float distance= vec.magnitude;	
	    transform.localScale = Vector3.Lerp (Vector3.one * minGlow, scale, Mathf.Clamp01 (distance * 0.35f));
	    mat.SetColor ("_TintColor",  glowColor * Mathf.Clamp (distance * 0.1f, minGlow, maxGlow));	
    }

}