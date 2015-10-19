using UnityEngine;
using System.Collections;

public class TriggerOnMouseOrJoystick : MonoBehaviour {

    public SignalSender startFireSignals;
    public SignalSender stopFireSignals;
	
	public GameObject ownerPlayer;

    private bool  state = false;

    #if UNITY_IPHONE || UNITY_ANDROID
        private Joystick[] joysticks;

        void Start (){
			joysticks = FindObjectsOfType (typeof(Joystick)) as Joystick[];	
        }
    #endif

    void Update (){
		if (!ownerPlayer.GetComponent<PlayerScript> ().isLocalPlayer)
			return;

    #if UNITY_IPHONE || UNITY_ANDROID
	    if (state == false && joysticks[0].tapCount > 0) {
		    startFireSignals.SendSignals (this);
		    state = true;
	    } else if (joysticks[0].tapCount <= 0) {
		    stopFireSignals.SendSignals (this);
		    state = false;
	    }	
    #else	
	    #if !UNITY_EDITOR && (UNITY_XBOX360 || UNITY_PS3)
		    // On consoles use the right trigger to fire
		    float fireAxis = Input.GetAxis("TriggerFire");
		    if (state == false && fireAxis >= 0.2f) {
			    startFireSignals.SendSignals (this);
			    state = true;
		    } else if (state == true && fireAxis < 0.2f) {
			    stopFireSignals.SendSignals (this);
			    state = false;
		    }
	    #else
//		    if (state == false && Input.GetMouseButtonDown (0)) {
		if (state == false && Input.GetKeyDown(KeyCode.Space)) {
			    startFireSignals.SendSignals (this);
			    state = true;
//		    }  else if (state == true && Input.GetMouseButtonUp (0)) {
		}  else if (state == true && Input.GetKeyUp(KeyCode.Space)) {
			    stopFireSignals.SendSignals (this);
			    state = false;
		    }
	    #endif
    #endif
    }

}