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
        if (!Input.anyKey && PlayerPrefs.GetInt("KeyDown",0) == 1)
        {
            PlayerPrefs.SetInt("KeyDown",0);
        }
		
		if (Input.GetAxis("Submit") == 1 && PlayerPrefs.GetInt("KeyDown",0) == 0)
		{
			if(currentScene == "Victory") SceneManager.LoadScene("LevelSelect");
		}
		if (Input.GetKey("escape") && PlayerPrefs.GetInt("KeyDown",0) == 0)
		{
			if(cancelSound != null) cancelSound.Play();			
			SceneManager.LoadScene("Start");
		}
		if(Input.GetKey("r") && PlayerPrefs.GetInt("KeyDown",0) == 0)
		{
			if(currentScene == "Credit") 
			{
				if(cancelSound != null) cancelSound.Play();	
				PlayerPrefs.DeleteAll();
			}
		}
		
	}
}
