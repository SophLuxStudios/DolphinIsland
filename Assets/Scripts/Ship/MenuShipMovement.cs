using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class MenuShipMovement : MonoBehaviour
{
    //make this class public
    public static MenuShipMovement menuShipMovement;

    //Serialized properties
    [SerializeField] private float shipForwardSpeed = 0f;
    [SerializeField] private float shipHorizontalSpeed = 0f;
    [SerializeField] private float smoothRotation = 5f;
    [SerializeField] private Transform paddle;

    //private properties
    float tiltAngle = 10f;

    //used components
    private Rigidbody rb;
    private Quaternion paddleStartRotation;

    //inputs
    public float horizontal;
    public float vertical;

    /*private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }*/

    private void Start()
    {
        paddle = transform.GetChild(0).Find("Paddle");
        paddleStartRotation = paddle.localRotation;
    }

    private void FixedUpdate()
    {
        // take inputs for PC
        //horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");  

        //take Tilt input
        if(PlayerPrefs.GetString("Controller") == "Tilt")
        {
            TiltInput();
        }

        //tilt the ship towards moving direction
        float tiltShip = horizontal * tiltAngle;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, tiltShip, 0),  Time.deltaTime * smoothRotation);

        //ship movement 
        transform.Translate (horizontal * shipHorizontalSpeed * Time.fixedDeltaTime, 0f, vertical * shipForwardSpeed * Time.fixedDeltaTime);

        // default direction
        float steer = 0;

        // steer direction [-1,0,1]
        steer = horizontal;

        //paddle animation
        switch(PlayerPrefs.GetInt("ShipVariationIndex", 0))
        {
            case 1:
                paddle.SetPositionAndRotation(paddle.position, transform.rotation * paddleStartRotation * Quaternion.Euler(paddle.rotation.x, 30f * steer, 0));
                break;
            case 2:
                paddle.SetPositionAndRotation(paddle.position, transform.rotation * paddleStartRotation * Quaternion.Euler(0, 0, 5f * steer));
                break;
            default:
                paddle.SetPositionAndRotation(paddle.position, transform.rotation * paddleStartRotation * Quaternion.Euler(0, 30f * steer, 0));
                break;
        }
    }
  
    //take TouchButtons input
    public void ButtonInput(int buttonHorizontalInput)
    {
        if(PlayerPrefs.GetString("Controller") == "Touch")
            horizontal = buttonHorizontalInput;
    }

    //take Tilt input
    private void TiltInput()
    {
        float tiltDirectionX = Input.acceleration.x;
        if(tiltDirectionX < -.15f)
        {
            horizontal = -1;
        }
        else if(tiltDirectionX > .15f)
        {
            horizontal = 1;
        }
        else
        {
            horizontal = 0;
        }

        float tiltDirectionY = Input.acceleration.y;
        if(tiltDirectionY < -.1f)
        {
            horizontal = -1;
        }
        else if(tiltDirectionY > .1f)
        {
            horizontal = 1;
        }
        else
        {
            horizontal = 0;
        }        
    }
}