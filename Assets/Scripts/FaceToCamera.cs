using UnityEngine;
using System.Collections;

public class FaceToCamera : MonoBehaviour {
	public Vector3 cameraDirection;
	public Camera camera;

	void Awake(){
		camera = Camera.main;
	}

	void Update()
	{
//		cameraDirection = Camera.main.transform.forward;
		if (camera) {
			cameraDirection = camera.transform.forward;
//			cameraDirection.y = 0;
			this.transform.rotation = Quaternion.LookRotation (cameraDirection);
		}
	}

}
