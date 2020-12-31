using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionsController : MonoBehaviour
{

    public GameObject[] collections;
    public GameObject[] collections_locked;
    public Text collectionLabel;
    public Text collectionDesc;
    public static int collectionNumber = 0;
    public AudioSource menuSelectAudio;
    public bool keyDown;
    private float newX;

    void Start()
    {
        collectionNumber = 0;
        // make unlocked collection visible
        for(int i = 0 ; i < collections.Length ; i++)
        {
            if(PlayerPrefs.GetInt(collections[i].GetComponent<CollectionObject>().collectionName, 0) == 1)
            {
                collections[i].GetComponent<CollectionObject>().unlocked = true;
                collections[i].SetActive(true);
                collections_locked[i].SetActive(false);
            }
        }

        //set default value
        if(collections[0].GetComponent<CollectionObject>().unlocked) 
        {
            collectionLabel.text = collections[0].GetComponent<CollectionObject>().collectionLabel;
            collectionDesc.text = collections[0].GetComponent<CollectionObject>().description;
        }
        else 
        {
            collectionLabel.text = "";
            collectionDesc.text = collections_locked[0].GetComponent<CollectionObject>().description;
        }
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
                    if (collectionNumber < collections.Length - 1){
                        menuSelectAudio.Play();
                        collectionNumber++;
                    }
                }
                else if(Input.GetAxis("Horizontal") < 0)
                {
                    if (collectionNumber > 0){
                        menuSelectAudio.Play();
                        collectionNumber --;
                    }               
                }
                newX = collectionNumber * 5f * -1;
                keyDown = true;
            }
            
            //update description text
            if (collections[collectionNumber].GetComponent<CollectionObject>().unlocked)
            {
                collectionLabel.text = collections[collectionNumber].GetComponent<CollectionObject>().collectionLabel;
                collectionDesc.text = collections[collectionNumber].GetComponent<CollectionObject>().description;
            }
            else
            {
                collectionLabel.text = "";
                collectionDesc.text = collections_locked[collectionNumber].GetComponent<CollectionObject>().description;
            }
        }
        else
        {
            keyDown = false;
        }

        transform.position = new Vector3(newX, levelObjPos.y, levelObjPos.z);
        
        
    }
}
