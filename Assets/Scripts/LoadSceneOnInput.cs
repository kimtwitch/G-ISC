using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnInput : MonoBehaviour {
	private string currentScene;
	public AudioSource cancelSound;

	// Use this for initialization
	void Start () {
        currentScene = SceneManager.GetActiveScene().name;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Submit") == 1)
		{
			if(currentScene == "Victory") SceneManager.LoadScene("LevelSelect");
		}
		if (Input.GetKey("escape"))
		{
			if(cancelSound != null) cancelSound.Play();			
			SceneManager.LoadScene("Start");
		}
		if(Input.GetKey("r"))
		{
			if(currentScene == "Credit") PlayerPrefs.DeleteAll();
		}
		
	}
}
