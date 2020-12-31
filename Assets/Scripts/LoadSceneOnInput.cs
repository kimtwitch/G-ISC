using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnInput : MonoBehaviour {
	private string currentScene;

	// Use this for initialization
	void Start () {
        Scene scene = SceneManager.GetActiveScene();
		currentScene = scene.name;
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentScene)
		{
			case "Victory":
				if (Input.GetAxis("Submit") == 1) {
					SceneManager.LoadScene("LevelSelect");
				}
				if (Input.GetKey("escape")) {
					SceneManager.LoadScene("Start");
				}
				break;

			case "SelectLevel":
				if (Input.GetKey("escape")) {
					SceneManager.LoadScene("Start");
				}
				break;
				
			case "Start":
				break;
				
			default:
				if (Input.GetKey("escape")) {
					SceneManager.LoadScene("Start");
				}
				break;
		}
		
	}
}
