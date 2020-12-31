using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] tall;
    public GameObject[] square;
    public GameObject[] backgroundItems;

    public GameObject obstacleParent;
    private bool spawning = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++) 
        {
            float scale = Random.Range(.1f,1f);
            CreateChildPrefab(backgroundItems[Random.Range(0, backgroundItems.Length)], obstacleParent, scale, "init");
        }
        for (int i = 0; i < 5; i++) 
        {
            float scale = Random.Range(.5f,2f);
            CreateChildPrefab(tall[Random.Range(0, tall.Length)], obstacleParent, scale, "init");
            CreateChildPrefab(square[Random.Range(0, square.Length)], obstacleParent, scale, "init");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (obstacleParent.GetComponent<ObstacleController>().moving && !spawning)
        {
            spawning = true;
            StartCoroutine(SpawnObstacles());
            StartCoroutine(SpawnObstacles());
            StartCoroutine(SpawnBackgrounds());
        }
    }

    IEnumerator SpawnObstacles()
    {
        while(true)
        {
            int waitingSecond;
            if (Random.value < 0.5) 
            {
                float scale = Random.Range(2f,4f);
                CreateChildPrefab(tall[Random.Range(0, tall.Length)], obstacleParent, scale, "");
                waitingSecond = Random.Range(2,6)/2;
            } 
            else
            {
                float scale = Random.Range(2f,4f);
                CreateChildPrefab(square[Random.Range(0, square.Length)], obstacleParent, scale, "");
                waitingSecond = Random.Range(2,6)/2;
            }

            yield return new WaitForSeconds(waitingSecond);
        }
    }
    IEnumerator SpawnBackgrounds()
    {
        while(true)
        {
            // Spawn background Items
            float scale = Random.Range(.5f,1.5f);
            CreateChildPrefab(backgroundItems[Random.Range(0, backgroundItems.Length)], obstacleParent, scale, "BG");
            
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }

    void CreateChildPrefab(GameObject child, GameObject parent, float scale, string mode)
    {
        Vector3 location = new Vector3(50, Random.Range(-15, 10), 21);
        if(mode == "init")
        {
            if (Random.value < 0.5)
            {
                location = new Vector3(Random.Range(-30, -3), Random.Range(-15, 10), 21);
            }
            else
            {
                location = new Vector3(Random.Range(3, 50), Random.Range(-15, 10), 21);
            }
            
        }
        if(mode == "BG")
        {
            location = new Vector3(50, Random.Range(-25, 25), Random.Range(17,25));
        }
        GameObject prefab = Instantiate(child, location, Quaternion.Euler(0f, Random.Range(0f, 180f), 0f));
        prefab.transform.localScale = new Vector3(scale, scale, scale);
        prefab.transform.parent = parent.transform;
    }
}
