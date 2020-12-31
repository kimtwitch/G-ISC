using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ProgressText : MonoBehaviour
{
    
    private Text text;
    private float dna;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        dna = ProgressTracker.dnaCollection;
        text.text = "DNA Particles Collected: " + Mathf.RoundToInt(dna);
    }
}
