using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorController : MonoBehaviour
{
    public GameObject tractorBeam;
    public AudioSource tractorBeamSound;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if left pressed : activate tractor beam
        if (Input.GetMouseButtonDown(0)){
            tractorBeamSound.volume = 1;
            //tractorBeamSound.Play();
            tractorBeam.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0)) {
            tractorBeamSound.volume = 0;
            //tractorBeamSound.Stop();
            tractorBeam.SetActive(false);
        }
    }
}
