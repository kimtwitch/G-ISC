using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishObject : MonoBehaviour
{
    public float moveHeight = 1.5f;
    public float newY;
    public float initYPos;
    public bool playerNearFinish;

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale > 0)
        {
            if(playerNearFinish)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1,15,15),Time.deltaTime/2);
            }

            float newY = Mathf.PingPong(Time.time, 1)*moveHeight;
            transform.position = new Vector3(transform.position.x, initYPos + newY, transform.position.z);
        }
        else
        {
            transform.position = transform.position;
        }
    }
}
