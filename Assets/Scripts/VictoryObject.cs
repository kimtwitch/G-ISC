using UnityEngine;

public class VictoryObject : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 40 * Time.deltaTime);
    }
}
