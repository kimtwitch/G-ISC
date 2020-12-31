using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Text))]
public class ResultText : MonoBehaviour
{
    private Text text;
    private int dnaCollected;
    private int dnaCollected_highScore;
    private int newHighScore;
    private int victoryLevel;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        dnaCollected = PlayerPrefs.GetInt("DNA", 0);
        newHighScore = PlayerPrefs.GetInt("newHighScore", 0);
        victoryLevel = PlayerPrefs.GetInt("Victory_Level", 0);

        Scene scene = SceneManager.GetActiveScene();
        
        switch(scene.name)
        {
            case "Endless":
                //Endless mode 
                dnaCollected_highScore = PlayerPrefs.GetInt("DNA_Endless", 0);
                break;

            default:
                //Level mode
                dnaCollected_highScore = PlayerPrefs.GetInt("DNA_Level"+victoryLevel, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Collected DNA Particles: " + dnaCollected + "\n";
        if (newHighScore == 1)
        {
            text.text += "** New ** ";
        }
        text.text += "High Score: " + dnaCollected_highScore;
    }
}
