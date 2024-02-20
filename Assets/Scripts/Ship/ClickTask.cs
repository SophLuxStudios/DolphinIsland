using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickTask : MonoBehaviour
{
    //UI
    [SerializeField] private Image taskCircle;

    //used class
    private ShipHealth shipHealth;
    private AudioManager audioManager;
    
    //used components
    [SerializeField] private Rigidbody rbShip;
    [SerializeField] private Rigidbody rbBoat;
    private GameObject dolphinGuerrilla;

    //private fields
    private float taskTimeLimit = 1.5f;
    private float taskTimer = 1.5f;
    private RectTransform tcRectTransform;
    private Vector2 leftPosition = new Vector2(-625, -25);
    private Vector2 centralPosition = new Vector2(0, 325);
    private Vector2 rightPosition = new Vector2(650, -25);
    private float thrust = 1100f;
    private bool taskStarted;
    private string attackedFrom;
    private int randomTaskCount;
    private bool dolphinShooedAway;
    [SerializeField] private int chasePosition;
    private bool chaseIsHappening;
    private Transform[] guerrillaPositions = new Transform[6];

    void Start()
    {
        shipHealth = GetComponent<ShipHealth>();
        audioManager = FindObjectOfType<AudioManager>();

        tcRectTransform = taskCircle.GetComponent<RectTransform>();

        rbShip = GetComponent<Rigidbody>();
        rbBoat = gameObject.transform.GetChild(0).GetComponent<Rigidbody>();

        dolphinGuerrilla = transform.Find("DolphinGuerrilla").gameObject;
        for(int i = 0; i < guerrillaPositions.Length; i++)
        {
            guerrillaPositions[i] = transform.Find("GuerrillaPositions").GetChild(i).transform;
        }

        randomTaskCount = Random.Range(3, 6);

        DolphinGuerrillaChase();
    }

    void Update()
    {
        
        if(chaseIsHappening && transform.position.z > chasePosition)
        {
            dolphinGuerrilla.SetActive(true);

            StartTask();

            chaseIsHappening = false;
        }

        if(taskStarted)
        {
            DolphinAttack();

            taskTimer -= Time.deltaTime;
            taskCircle.gameObject.SetActive(true);
            taskCircle.fillAmount = taskTimer / taskTimeLimit;

            if(taskTimer <= 0)
            {
                ResetAndDeactivateTaskCircle();

                if(shipHealth.alive)
                {
                    Debug.Log("Ship is alive do again");
                    TaskFailed();
                }
                else
                {
                    //if the ship is not alive deactivate
                    dolphinGuerrilla.SetActive(false);
                }
            }
        }

        if(dolphinShooedAway)
        {
            dolphinGuerrilla.transform.Translate(Vector3.back * 3 * Time.deltaTime);

            float distance = Vector3.Distance(dolphinGuerrilla.transform.position, transform.position);

            if(distance > 25)
            {
                dolphinGuerrilla.SetActive(false);

                dolphinShooedAway = false;
            }
        }
    }

    private void DolphinGuerrillaChase()
    {
        // 75% chance of chase
        int isDolphinGuerrillaAround = Random.Range(1,5);
        
        if(isDolphinGuerrillaAround != 1)
        {
            chaseIsHappening = true;

            //randomize chase position between 2000 and 6000
            chasePosition = Random.Range(2000, 6001);
        }
        else
        {
            Debug.Log("there is no dolphin guerrilla around");
        }
    }

    private void DolphinAttack()
    {
        switch(attackedFrom)
        {
            case "left":
                dolphinGuerrilla.transform.position = guerrillaPositions[0].position;
                break;
            case "center":
                dolphinGuerrilla.transform.position = guerrillaPositions[1].position;
                break;
            case "right":
                dolphinGuerrilla.transform.position = guerrillaPositions[2].position;
                break;
        }
    }

    private void StartTask()
    {
        audioManager.Play("DolphinNoise");

        int taskCircleIndex = Random.Range(1,4);

        switch(taskCircleIndex)
        {
            case 1:
                taskCircle.rectTransform.anchoredPosition = leftPosition;
                attackedFrom = "left";
                break;
            case 2:
                taskCircle.rectTransform.anchoredPosition = centralPosition;
                attackedFrom = "center";
                break;
            case 3:
                taskCircle.rectTransform.anchoredPosition = rightPosition;
                attackedFrom = "right";
                break;
        }

        taskStarted = true;
    }

    private void ResetAndDeactivateTaskCircle()
    {
        taskTimer = taskTimeLimit;
        taskCircle.fillAmount = 1f;
        taskCircle.gameObject.SetActive(false);

        taskStarted = false;
    }

    private void TaskFailed()
    {
        switch(attackedFrom)
        {
            case "left":
                rbShip.AddRelativeForce(Vector3.right * thrust);
                rbBoat.AddRelativeForce(Vector3.right * thrust);
                //rb.AddRelativeForce(Vector3.right * thrust);
                break;
            case "center":
                rbShip.AddRelativeForce(Vector3.forward * thrust);
                rbBoat.AddRelativeForce(Vector3.forward * thrust);
                //rb.AddRelativeForce(Vector3.forward * thrust);
                break;
            case "right":
                rbShip.AddRelativeForce(Vector3.left * thrust);
                rbBoat.AddRelativeForce(Vector3.left * thrust);
                //rb.AddRelativeForce(Vector3.left * thrust);
                break;
        }

        //Debug.Log("Task failed.");

        if(transform.position.z < 7200)
        {
            StartTask();
        }
        else
        {
            dolphinShooedAway = true;
        }
    }

    public void TaskCompleted()
    {
        ResetAndDeactivateTaskCircle();

        randomTaskCount--;

        if(randomTaskCount == 0 || transform.position.z > 7200)
        {
            dolphinShooedAway = true;
        }
        else
        {
            Invoke("StartTask", 2);
        }

        //Debug.Log("Task completed.");
    }
}