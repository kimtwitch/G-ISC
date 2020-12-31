using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objectives : MonoBehaviour
{
    public string collectionName;
    public float thrust = -1.0f;
    public GameObject playerObject;
    public GameObject TractorBeam;
    public GameObject ObstacleController;
	public ParticleSystem explosion;
	public AudioSource explosionSound;
	public AudioSource hurtSound;
    public float health = 100;
    public bool trigger;
    public bool primaryObject;

	private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -30f || transform.position.y < -30f)
        {
            SampleLost();
        }

        if (!trigger && ObstacleController.GetComponent<ObstacleController>().moving)
        {
            transform.position = transform.position + new Vector3( ObstacleController.GetComponent<ObstacleController>().speed * Time.deltaTime, 0, 0);
        }

        if (!TractorBeam.activeInHierarchy)
        {
            trigger = false;
        }
    }

    void onCollisionEnter(Collision hit)
    {
        hurtSound.Play();
        //float impulseAmount = (hit.impulse.x + hit.impulse.y) + 100;
        //if (health - impulseAmount > 0)
        //{
        //    health = health - impulseAmount;
        //}
        //else
        //{
        //    health = 0;
        //    Explode();
        //}
        
    }

    void OnTriggerEnter(Collider other)
    {
        // Destroy objective if it touches antimatter zone
        if (other.gameObject.tag == "DestroyZone")
        {
            Explode();
        }
        // Victory
        if (other.gameObject.tag == "Finish")
        {
            PlayerPrefs.SetInt(collectionName, 1);
            Destroy(gameObject);

            if(primaryObject)
            {
                ProgressTracker.victory = true;
                SceneManager.LoadScene("Victory");
            }
        }

    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "TractorBeam")
        {
            trigger = true;

            float diffX = Mathf.Abs(transform.position.x - playerObject.transform.position.x);
            int directionX = transform.position.x > playerObject.transform.position.x ? -1 : transform.position.x < playerObject.transform.position.x ? 1 : 0;

		    rb.velocity = new Vector3(diffX * directionX * thrust, thrust, 0);

            if(primaryObject && !ObstacleController.GetComponent<ObstacleController>().atFinish)
            {
                // Collect DNA while being tractored
                ProgressTracker.dnaCollection = ProgressTracker.dnaCollection + Time.deltaTime * 10;
            }
        }
        
    }

    void SampleLost(){
        Destroy(gameObject);
        health = 0;

        if(primaryObject)
        {
            //Game over only when primary object is lost
            ObstacleController.GetComponent<ObstacleController>().moving = false;
            ProgressTracker.gameOver = true;
        }
    }
	void Explode() {
		explosionSound.Play();

		explosion.transform.position = transform.position;
		explosion.Play();
		
		SampleLost();
	}

}
