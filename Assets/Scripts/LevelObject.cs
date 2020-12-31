using UnityEngine;

public class LevelObject : MonoBehaviour
{
    float rotateSpeed = 20;
    public float moveHeight = 1.5f;
    
    public bool unlocked;
    public bool passed;

    public float newY;
    public float yConst;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(passed)
        {
            float newY = Mathf.PingPong(Time.time, 1);
            transform.position = new Vector3(transform.position.x, newY + yConst, transform.position.z);
            transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
            
        }
        else if (unlocked) {
            transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}
