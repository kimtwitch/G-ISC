using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public int level;
    public int unlockedLevel = 0;
    public int maxLevel = 2;
    public GameObject[] levelObjects;
    public GameObject[] levelObjects_locked;
    public string[] levelDescriptions;
    public AudioSource menuSelectAudio;
    public Text levelDesc;
    public bool keyDown;
    private float newX;

    // Start is called before the first frame update
    void Start()
    {
        int passedLevel = PlayerPrefs.GetInt("Passed_Level", -1);
        for(int i = 0 ; i <= passedLevel ; i++)
        {
            levelObjects[i].GetComponent<LevelObject>().passed = true;
        }

        level = PlayerPrefs.GetInt("Unlocked_Level", 0);
        unlockedLevel = level;
        for(int i = 0 ; i <= level ; i++)
        {
            levelObjects[i].SetActive(true);
            levelObjects[i].GetComponent<LevelObject>().unlocked = true;
            if(i>0)
            {
                levelObjects_locked[i-1].SetActive(false);
            }
        }

        newX = level * 5f * -1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 levelObjPos = transform.position;

        if(Input.GetAxis("Horizontal") != 0)
        {
            if(!keyDown)
            {
                if(Input.GetAxis("Horizontal") > 0)
                {
                    if (level < unlockedLevel){
                        menuSelectAudio.Play();
                        level++;
                    }
                }
                else if(Input.GetAxis("Horizontal") < 0)
                {
                    if (level > 0){
                        menuSelectAudio.Play();
                        level --;
                    }               
                }
                newX = level * 5f * -1;
                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }
        transform.position = new Vector3(newX, levelObjPos.y, levelObjPos.z);
        levelDesc.text = levelDescriptions[level];
        
        
		if (Input.GetAxis("Submit") == 1) {
            if(level < maxLevel)
            {
                SceneManager.LoadScene("Level"+level);
            }
		}
    }
}
