using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    //private fields
    private float speed = 30f;
    private bool thrown = false;
    private float thrust = -1300f;
    private bool harpoonCommunicationFlag = false;

    //dolphin components
    private GameObject dolphin;
    private Animator dolphin_Animator;

    //used components
    private Rigidbody rb;

    //used Class
    private DolphinHunt dolphinHunt;

    void Awake()
    {
        dolphin = GameObject.Find("Dolphin");

        dolphin_Animator = dolphin.GetComponent<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        dolphinHunt = GameObject.FindObjectOfType<DolphinHunt>();
    }

    void FixedUpdate()
    {
        var step = speed * Time.deltaTime;

        if(!thrown)
        {
            transform.localEulerAngles = new Vector3(93f, Mathf.PingPong(Time.fixedTime * speed, 40) -20 , 0f);
        }
    }

    public void Throw()
    {
        thrown = true;

        rb.AddRelativeForce(this.transform.forward * thrust);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Dolphin")
        {
            Debug.Log("The dolphin is hit.");
            rb.Sleep();
            dolphin_Animator.enabled = false;

            dolphin.transform.Rotate(0.0f, 0.0f, 40.0f, Space.Self);

            if(!harpoonCommunicationFlag)
            {
                dolphinHunt.HarpoonHitDolphin();
                harpoonCommunicationFlag = true;
            }
            //Destroy(this.gameObject);
        }

        if(collision.gameObject.name == "TerrainPrefab")
        {
            if(!harpoonCommunicationFlag)
            {
                dolphinHunt.HarpoonMissed();
                harpoonCommunicationFlag = true;
            }

            Debug.Log("Harpoon missed the dolphin.");
            rb.Sleep();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            //Destroy(this.gameObject);
        }
    }
}