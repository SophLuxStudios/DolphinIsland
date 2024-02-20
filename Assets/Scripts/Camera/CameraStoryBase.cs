using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraStoryBase : MonoBehaviour
{
    //used classes
    private BaseMenu baseMenu;

    //camera properties
    public float cameraSpeed = 4f;
    public float stepModifier = 30f;

    //Position Vectors
    private Vector3 targetPosition;
    private Vector3 startingPosition = new Vector3(115f, 30f, 55f);
    private Vector3 mapPosition = new Vector3(53f, 25f, 90f);
    private Vector3 shelfPosition = new Vector3(65f, 16f, 100f);
    private Vector3 shipPosition = new Vector3(55f,16f,65f);

    //Quaternions
    private Quaternion targetRotation;
    private Quaternion startingRotation = Quaternion.Euler(9, -70, 0);
    private Quaternion mapRotation = Quaternion.Euler(75, -40, 0);
    private Quaternion shelfRotation = Quaternion.Euler(20, -60, 0);
    private Quaternion shipRotation = Quaternion.Euler(20, -80, 0);

    //used booleans
    bool startedMoving = false;
    bool reachedPosition = false;
    public bool isAtMapPosition = false;
    public bool setSail = false;

    //used UI
    [SerializeField] private GameObject shipMenuPanel;
    [SerializeField] private GameObject capacityPanel;

    void Awake()
    {
        baseMenu = GameObject.FindObjectOfType<BaseMenu>();
    }

    void FixedUpdate()
    {
        if(startedMoving && !reachedPosition)
        {
            float step =  cameraSpeed * Time.fixedDeltaTime;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step * stepModifier);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step * stepModifier);


            if (Vector3.Distance(transform.position, targetPosition) < 0.01f && 
                Quaternion.Angle(transform.rotation, targetRotation) < 0.01f)
            {
                reachedPosition = true;
                startedMoving = false;
                isAtMapPosition = false;

                if(transform.position == mapPosition)
                {
                    isAtMapPosition = true;
                }
                else if(transform.position == shelfPosition)
                {
                    capacityPanel.SetActive(true);
                }
                else if(transform.position == shipPosition)
                {
                    shipMenuPanel.SetActive(true);

                    if(setSail)
                    {
                        baseMenu.LoadStoryMode();
                    }
                }
            }
        }
    }

    public void MoveTowardsMap()
    {
        startedMoving = true;
        reachedPosition = false;

        targetPosition = mapPosition;
        targetRotation = mapRotation;
    }

    public void MoveTowardsShelf()
    {
        startedMoving = true;
        reachedPosition = false;

        targetPosition = shelfPosition;
        targetRotation = shelfRotation;
    }

    public void MoveBackwards()
    {
        startedMoving = true;
        reachedPosition = false;

        targetPosition = startingPosition;
        targetRotation = startingRotation;
    }

    public void MoveToShip()
    {
        startedMoving = true;
        reachedPosition = false;

        targetPosition = shipPosition;
        targetRotation = shipRotation;
    }
}