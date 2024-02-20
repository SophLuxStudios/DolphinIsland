using UnityEngine;

public class MenuWaveFollow : MonoBehaviour
{
    //private properties
    private Transform target;
    private Vector3 offset;
    private int doNotFollow = 1;

    private const float forwardSpeed = 8f;
    private const float horizontalSpeed = 6f;

    //inputs
    private float horizontal;
    private float vertical;

    //used ship's script
    private GameObject ship;
    private MenuShipMovement menuShipMovement;

    private void Awake()
    {
        ship = GameObject.FindGameObjectWithTag("Ship");
        target = ship.transform;
        menuShipMovement = ship.GetComponent<MenuShipMovement>();
    }

    private void Start()
    {
        offset = transform.position - target.transform.position;
    }

    private void OnTriggerEnter(Collider Ship)
    {
        doNotFollow = 1;
    }

    private void OnTriggerExit(Collider Ship)
    {
        doNotFollow = 0;
    }

    private void FixedUpdate()
    {
        if(doNotFollow == 0)
        {
            //wave movement 
            transform.Translate (menuShipMovement.horizontal * horizontalSpeed * Time.fixedDeltaTime, 0f, 
            menuShipMovement.vertical * forwardSpeed * Time.fixedDeltaTime);
        }
    }
}