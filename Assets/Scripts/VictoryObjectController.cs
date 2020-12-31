using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryObjectController : MonoBehaviour
{
    public GameObject[] victoryObjects;
    private int victoryLevel;
    private float rotateSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        victoryLevel = PlayerPrefs.GetInt("Victory_Level", 0);
        victoryObjects[victoryLevel].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        victoryObjects[victoryLevel].transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
