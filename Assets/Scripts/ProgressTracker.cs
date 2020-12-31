using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressTracker : MonoBehaviour
{
    public static float dnaCollection;
    public static bool gameOver;
    public static bool victory;
    public static bool newHighScore;
    // Start is called before the first frame update
    void Start()
    {
        dnaCollection = 0f;
        gameOver = false;
        victory = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver || victory)
        {
            PlayerPrefs.SetInt("DNA", Mathf.RoundToInt(dnaCollection));

            // Set high score for each play mode
            Scene scene = SceneManager.GetActiveScene();

            if(gameOver && scene.name == "Endless")
            {
                //Endless mode
                if (PlayerPrefs.GetInt("DNA_Endless", 0) < PlayerPrefs.GetInt("DNA", 0))
                {
                    PlayerPrefs.SetInt("newHighScore", 1);
                    PlayerPrefs.SetInt("DNA_Endless", Mathf.RoundToInt(dnaCollection));
                }
                else
                {
                    PlayerPrefs.SetInt("newHighScore", 0);
                }
            }
            
            if(victory)
            {
                //Level mode
                string currentLevel = scene.name.Substring(5);
                int currentLevelInt = int.Parse(currentLevel);

                PlayerPrefs.SetInt("Victory_Level", currentLevelInt);

                if(PlayerPrefs.GetInt("Passed_Level" , 0) <= currentLevelInt)
                {
                    // level passed
                    PlayerPrefs.SetInt("Passed_Level", currentLevelInt);
                    // New level unlocked
                    PlayerPrefs.SetInt("Unlocked_Level", currentLevelInt + 1);
                }
                
                if (PlayerPrefs.GetInt("DNA_Level"+currentLevel, 0) < PlayerPrefs.GetInt("DNA", 0))
                {
                    PlayerPrefs.SetInt("newHighScore", 1);
                    PlayerPrefs.SetInt("DNA_Level"+currentLevel, Mathf.RoundToInt(dnaCollection));
                }
                else
                {
                    PlayerPrefs.SetInt("newHighScore", 0);
                }
            }
        }
    }
}
