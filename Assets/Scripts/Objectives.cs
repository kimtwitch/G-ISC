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
    public bool primaryObject;
    public bool trigger;
    public bool triggerStay;
    public bool targeted;
    public float gravityDamper;
    public float inertiaDamper;

	private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
Debug.Log(inertiaDamper);
        if (transform.position.x < -30f || transform.position.y < -30f)
        {
            SampleLost();
        }

        if (!trigger && ObstacleController.GetComponent<ObstacleController>().moving)
        {
            transform.position = transform.position + new Vector3( ObstacleController.GetComponent<ObstacleController>().speed * Time.deltaTime, 0, 0);
        }

        if ((!TractorBeam.activeInHierarchy) || (TractorBeam.activeInHierarchy && rb.velocity.y <= Physics.gravity.y))
        {
            if(inertiaDamper<1 && targeted && ObstacleController.GetComponent<ObstacleController>().moving)
            {
                float inertia = Mathf.Lerp(0, ObstacleController.GetComponent<ObstacleController>().speed, inertiaDamper);
                inertiaDamper += Time.deltaTime;
                transform.position = transform.position + new Vector3( inertia * Time.deltaTime, 0, 0);
            }
            else
            {
                trigger = false;
                targeted = false;
            }
        }
        //else if(rb.velocity.y <= Physics.gravity.y )
        //{
        //    trigger = false;
        //}
        else if(!triggerStay && targeted && trigger)
        {
            tractorTarget(false);
        }
    }

    void onCollisionEnter(Collision hit)
    {
        //hurtSound.Play();
        Debug.Log(hit.collider.name);
        Explode();
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

        if (other.gameObject.tag == "TractorBeam")
        {
            targeted = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        triggerStay = false;
        gravityDamper = 0;
        inertiaDamper = 0;
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "TractorBeam")
        {
            trigger = true;
            triggerStay = true;
            tractorTarget(true);

            if(primaryObject && !ObstacleController.GetComponent<ObstacleController>().atFinish)
            {
                // Collect DNA while being tractored
                ProgressTracker.dnaCollection = ProgressTracker.dnaCollection + Time.deltaTime * 10;
            }
        }
        
    }

    void tractorTarget(bool goesUp)
    {
        float diffX = Mathf.Abs(transform.position.x - playerObject.transform.position.x);
        float directionX = transform.position.x > playerObject.transform.position.x ? -1.5f : transform.position.x < playerObject.transform.position.x ? 1 : 0;
        float directionY = transform.position.y > playerObject.transform.position.y ? Physics.gravity.y : transform.position.y < playerObject.transform.position.y ? 1 : 0;
        
        if(goesUp)
        {
            rb.velocity = new Vector3(diffX * directionX * thrust, directionY * thrust, 0);
        }
        else 
        {
            float gravityAmount = Mathf.Lerp(0, Physics.gravity.y, gravityDamper);
            if(gravityDamper<1 && directionY > 0)
            {
                //Slowly adjust gravity amount from 0 to global gravity (-9.8)
                gravityDamper += Time.deltaTime;
                rb.velocity = new Vector3(diffX * directionX , gravityAmount, 0);
            }
            else
            {
                rb.velocity = new Vector3(diffX * directionX , Physics.gravity.y, 0);
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
