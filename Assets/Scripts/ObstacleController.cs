using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public bool moving = false;
    public float speed = -10.0f;
    public float inertialDampener = 0;
    public bool atFinish = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && !ProgressTracker.gameOver && !atFinish){
            moving = true;
        }

		// horizontal axis is either left or right or a and d on the keyboard, among others
		if (moving)
        {
            foreach(Transform child in transform)
            {
                if(child.tag == "Finish")
                {

                    if( child.position.x < 30)
                    {
                        // slow down obstacles when finish is near
                        inertialDampener = child.position.x / ((31 - child.position.x)*180);
                        child.GetComponent<FinishObject>().playerNearFinish = true;
                    }
                    if( child.position.x < 8 && !atFinish)
                    {
                        atFinish = true;
                    }
                }

                child.position = child.position + new Vector3( speed * Time.deltaTime, 0, 0);
            }

            // increase speed over time
            speed = (speed + Time.deltaTime * -0.1f) + inertialDampener;

            if (inertialDampener > Mathf.Abs(speed) || atFinish) {
                moving = false;
                atFinish = true;
            }
        }
        else
        {
            foreach(Transform child in transform)
            {
                child.position = child.position;
            }
        }

        
        foreach(Transform child in transform)
        {
            if (child.position.x < -40)
            {
                Destroy(child.gameObject);
            }
        }

    }

    void stop()
    {
        moving = false;
    }
}
