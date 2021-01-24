using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public GameObject PauseUI;
	public GameObject GameOverUI;
	public GameObject InGameUI;
	public AudioSource pauseSound;
	public AudioSource resetSound;
	public AudioSource cancelSound;

	// Use this for initialization
	void Start () 
	{
		Cursor.lockState = CursorLockMode.Confined;
	}

	// Update is called once per frame
	void Update () 
	{
		
		if (!ProgressTracker.gameOver) 
		{
			if (Input.GetKeyUp("escape")) 
			{
				pauseSound.Play();
				// pause & resume
				if (Time.timeScale == 1.0f)
				{
					Time.timeScale = 0f;
					PauseUI.SetActive(true);
					Cursor.lockState = CursorLockMode.None;
				}
				else
				{
					Time.timeScale = 1.0f;
					PauseUI.SetActive(false);
					Cursor.lockState = CursorLockMode.Confined;
				}
					
			}
			
			if(Input.GetKey("r") && Time.timeScale == 0f)
			{
				resetSound.Play();
				Time.timeScale = 1.0f;
				Scene scene = SceneManager.GetActiveScene();
				SceneManager.LoadScene(scene.name);
			}
		}
		else
		{
        	GameOverUI.SetActive(true);
			InGameUI.SetActive(false);
			if (Input.GetAxis("Cancel") == 1) 
			{
				cancelSound.Play();
				SceneManager.LoadScene("Start");
			}
			if (Input.GetAxis("Submit") == 1) {
            	PlayerPrefs.SetInt("KeyDown",1);
				resetSound.Play();
				Scene scene = SceneManager.GetActiveScene();
				SceneManager.LoadScene(scene.name);
			}
		}
	}
}
