using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    public int index;
    public bool keyDown;
    public int maxIndex;
    public AudioSource menuSelectAudio;

    // Start is called before the first frame update
    void Start()
    {
        menuSelectAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") != 0)
        {
            if(!keyDown)
            {
                menuSelectAudio.Play();
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
            Application.Quit();
        }
        
		if (Input.GetAxis("Submit") == 1) {
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
