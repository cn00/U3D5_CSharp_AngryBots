﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//[RequireComponent (typeof (GUITexture))]
public class ShowFps : MonoBehaviour {
	
	private GUIText gui;
	
	private float updateInterval = 1.0f;
	private float lastInterval; // Last interval end time
	private int frames = 0; // Frames over current interval
	
	void Start()
	{
		lastInterval = Time.realtimeSinceStartup;
		frames = 0;
	}
	
	void OnDisable ()
	{
		if (gui)
			DestroyImmediate (gui.gameObject);
	}

	// Update is called once per frame
	void Update () {
		#if !UNITY_FLASH
		++frames;
		var timeNow = Time.realtimeSinceStartup;
		if (timeNow > lastInterval + updateInterval)
		{
			if (!gui)
			{
				GameObject go = new GameObject("FPS Display", typeof(GUIText));
				go.hideFlags = HideFlags.HideAndDontSave;
				go.transform.position = new Vector3(0,0,0);
				gui = go.GetComponent<GUIText>();
				gui.pixelOffset = new Vector2(5,55);
			}
			float fps = frames / (timeNow - lastInterval);
			float ms  = 1000.0f / Mathf.Max (fps, 0.00001f);
			gui.text = ms.ToString("f1") + "ms\n" + fps.ToString("f2") + "FPS";
			frames = 0;
			lastInterval = timeNow;
		}
		#endif

	}
}
