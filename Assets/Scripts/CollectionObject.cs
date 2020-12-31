using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionObject : MonoBehaviour
{
    public bool unlocked;
    public bool selected;
    public int collectionIndex;
    public string collectionLabel = "";
    public string description = "This sample hasn't been collected yet.";
    public string collectionName = "Unidentified Sample";
    float rotateSpeed = 20;
    public int collectionUnlocked;
    private Quaternion initRot;


    // Start is called before the first frame update
    void Start()
    {
        initRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (CollectionsController.collectionNumber == collectionIndex)
        {
            if(!selected) selected = true;
        }
        else
        {
            if(selected) 
            {
                selected = false;
                transform.rotation = initRot;
            }
        }

        if(selected)
        {
            transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}
