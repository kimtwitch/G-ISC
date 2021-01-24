using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnInput : MonoBehaviour {
	private string currentScene;
	public AudioSource cancelSound;
	private bool interSceneInputHandled;
	private Coroutine inputHandlerCoroutine;

	// Use this for initialization
	void Start () {
        currentScene = SceneManager.GetActiveScene().name;
		interSceneInputHandled = false;
		inputHandlerCoroutine = StartCoroutine(HandleIntersceneInput());
	}

	private IEnumerator HandleIntersceneInput()
	{
		yield return new WaitForSeconds(.5f);
		interSceneInputHandled = true;
		StopCoroutine(inputHandlerCoroutine);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Submit") == 1 && interSceneInputHandled)
		{
			if(currentScene == "Victory") 
			{
				SceneManager.LoadScene("LevelSelect");
			}
		}
		if (Input.GetKey("escape") && interSceneInputHandled)
		{
			if(cancelSound != null) cancelSound.Play();			
			SceneManager.LoadScene("Start");
		}
		if(Input.GetKey("r") && interSceneInputHandled )
		{
			if(currentScene == "Credit") 
			{
				if(cancelSound != null) cancelSound.Play();	
				PlayerPrefs.DeleteAll();
			}
		}
		
	}
}
