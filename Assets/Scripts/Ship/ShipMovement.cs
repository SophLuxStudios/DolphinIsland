using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent (typeof(Rigidbody))]
public class ShipMovement : MonoBehaviour
{
    //make class public
    public static ShipMovement shipMovement;

    //public properties
    public float shipForwardSpeed;
    [HideInInspector] public float speedIncreasePerPoint = 0.3f;

    //public hided properties
    [HideInInspector] public bool sailingEnded = false;
    [HideInInspector] public bool isHuntEnded = false;
    [HideInInspector] public bool chasingDolphin = false;
    [HideInInspector] public bool failedToFindDolphin = false;

    //private fields
    private float shipHorizontalSpeed = 17f;
    private float smoothRotation = 5f;
    private float step;
    private const float tiltAngle = 10f;
    private Vector3 dockingPosition = new Vector3(22f, 0.2f, 7551f);
    private Vector3 undockingPosition = new Vector3(0f, 0.2f, 7600f);
    private float dolphinGotAwayPosition = 8400f;
    private bool levelIsRunning = false;
    int isPortAndMapActivated = 0;

    //Serialized components
    [SerializeField] private Transform paddle;

    //used components
    private Rigidbody rb;
    private Quaternion paddleStartRotation;

    //used scripts
    private ShipHealth shipHealth;
    private Port port;
    private DolphinEncounter dolphinEncounter;
    [SerializeField] private DolphinMovement dolphinMovement;

    //inputs
    private float horizontal;

    //Ship Objects
   [SerializeField] private GameObject waterTrial;

    //Dock Objects
    [SerializeField] private GameObject mapParchment;
    [SerializeField] private GameObject portPanel;
    [SerializeField] private GameObject dockPanel;
    
    //Moving Buttons
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;
    
    //Mode Boolean
    bool inStoryMode;

    //UI
    [SerializeField] private GameObject coinImage;
    [SerializeField] private TMP_Text xCurrencyText;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        

        rb.useGravity = false;

        shipHealth = GetComponent<ShipHealth>();
        port = GameObject.FindObjectOfType<Port>();
        dolphinEncounter = GameObject.FindObjectOfType<DolphinEncounter>();

        waterTrial = transform.Find("WaterTrial").gameObject;

        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName("StoryMode"))
        {
            inStoryMode = true;
        }

        if(inStoryMode)
        {
            shipForwardSpeed = 55;
        }
        else
        {
            shipForwardSpeed = 45;
        }
    }

    private void Start()
    {
        paddle = transform.GetChild(0).Find("Paddle");
        paddleStartRotation = paddle.localRotation;
    }

#if (UNITY_EDITOR)
    void Update()
    {
        if (Input.GetKeyDown("left"))
        {
            horizontal = -1;
        }

        if (Input.GetKeyUp("left"))
        {
            horizontal = 0;
        }

        if (Input.GetKeyDown("right"))
        {
            horizontal = 1;
        }

        if (Input.GetKeyUp("right"))
        {
            horizontal = 0;
        }
    }
#endif
    private void FixedUpdate()
    {
        if(inStoryMode)
        {
            if(transform.position.z > 7500)
            {
                //Deactivate waterTrial
                waterTrial.SetActive(false);

                if(!chasingDolphin)
                {

                    step =  20f * Time.fixedDeltaTime;

                    horizontal = 0;
                    shipForwardSpeed = 0f;
                }
                else if(chasingDolphin && !isHuntEnded)
                {
                    shipForwardSpeed = 35f;
                }
                else if(isHuntEnded)
                {
                    step = 0;
                    shipForwardSpeed = 0f;
                }
                
                if(transform.position.z < dockingPosition.z)
                {
                    sailingEnded = true;
                    transform.position = Vector3.MoveTowards(transform.position, dockingPosition, step);
                }
                else if(transform.position == dockingPosition)
                {
                    if(isPortAndMapActivated == 0)
                    {
                        DisableMovingButtons();

                        Discover();

                        mapParchment.SetActive(true);

                        if(port.portContains == "dock" && !failedToFindDolphin)
                        {
                            dockPanel.SetActive(true);
                        }
                        else
                        {
                            portPanel.SetActive(true);
                        }

                        coinImage.SetActive(true);

                        ScoreManager.scoreManager.RegisterCollected();

                        isPortAndMapActivated = 1;
                    }

                    DisplayCurrencyUI();
                }
                else if(transform.position.z > dockingPosition.z)
                {
                    transform.position = Vector3.MoveTowards(transform.position, undockingPosition, step);

                    if(!levelIsRunning &&
                       !dolphinEncounter.encounter &&
                       transform.position.z > (dockingPosition.z + 25f))
                    { 
                        StartCoroutine(LoadAsynchronously("LoadingScene"));
                    }

                    if(dolphinEncounter.encounter &&
                      !dolphinEncounter.alreadyEncountered && 
                      transform.position.z == undockingPosition.z)
                    {
                        dolphinEncounter.ShipAtEncounterPosition();
                    }
                }

                if(transform.position.z > dolphinGotAwayPosition)
                {
                    if(!isHuntEnded)
                    {
                        dolphinMovement.DolphinGotAway();
                        
                        isHuntEnded = true;
                    }
                }
            }
        }

        //take Tilt input
        if(PlayerPrefs.GetString("Controller", "Touch") == "Tilt")
        {
            TiltInput();
        }

        // default direction
        float steer;

        // steer direction [-1,0,1]
        steer = horizontal;

        //alive check: if not alive return
        if (!shipHealth.alive)
        {
            horizontal = 0;
            return;
        }

        //tilt the ship towards moving direction
        var tiltShip = steer * tiltAngle;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, tiltShip, 0),  Time.deltaTime * smoothRotation);

        //ship movement
        var horizontalMove = horizontal * shipHorizontalSpeed * Time.fixedDeltaTime;
        var forwardMove = shipForwardSpeed * Time.fixedDeltaTime;
        transform.Translate (horizontalMove, 0f, forwardMove);

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

    public void UndockShip()
    {
        transform.position = Vector3.MoveTowards(transform.position, undockingPosition, step);
    }

    public void MoveOnMap()
    {
        StartCoroutine(LoadAsynchronously("LoadingScene"));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        levelIsRunning = true;

        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneName);

        while(!loadingOperation.isDone)
        {
            yield return null;
        }
    }

    //take TouchButtons input
    public void ButtonInput(int buttonHorizontalInput)
    {
        if(PlayerPrefs.GetString("Controller", "Touch") == "Touch")
            horizontal = buttonHorizontalInput;
    }

    private void DisableMovingButtons()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }

    //take Tilt input
    private void TiltInput()
    {
        var tiltDirectionX = Input.acceleration.x;
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
    }

    private void Discover()
    {
        string locationName = "DiscoveredLocation" + PlayerPrefs.GetInt("ShipLocatedAt");

        PlayerPrefs.SetInt(locationName , 1);
    }

    void DisplayCurrencyUI()
    {
        xCurrencyText.text = "X " + PlayerPrefs.GetInt("Currency").ToString();
    }
}