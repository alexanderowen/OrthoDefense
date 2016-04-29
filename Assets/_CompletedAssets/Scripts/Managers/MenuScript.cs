﻿using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuScript : MonoBehaviour {

	void Start(){}

	public void newGame()
	{
		SceneManager.LoadScene ("Level 01 5.x");
	}

	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
