using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    public int index;
    public bool keyDown;
    public int maxIndex;
    public AudioSource menuSound;
    public AudioSource selectSound;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        menuSound = GetComponent<AudioSource>();
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") != 0)
        {
            if(!keyDown)
            {
                menuSound.Play();
                if(Input.GetAxis("Vertical") < 0)
                {
                    if (index < maxIndex){
                        index++;
                    } else {
                        index = 0;
                    }
                }
                else if(Input.GetAxis("Vertical") > 0)
                {
                    if (index > 0){
                        index --;
                    } else {
                        index = maxIndex;
                    }                    
                }
                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }

        
        if (Input.GetKey("escape")) {
            if (sceneName == "Start") Application.Quit();
        }
        
		if (Input.GetAxis("Submit") == 1) {
            selectSound.Play();
            switch(index)
            {
                case 0:
                    //Select Level
			        SceneManager.LoadScene("LevelSelect");
                    break;
                case 1:
                    //Endless
			        SceneManager.LoadScene("Endless");
                    break;
                case 2:
                    //Collections                    
			        SceneManager.LoadScene("Collections");
                    break;
                case 3:
                    //Credit
			        SceneManager.LoadScene("Credit");
                    PlayerPrefs.DeleteAll();
                    break;
            }
		}
    }
}
