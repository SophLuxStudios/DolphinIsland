using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinMovement : MonoBehaviour
{
    //private hunt properties
    private bool dolphinGetsAway = false;
    [SerializeField] private bool isDolphinHit = false;

    //private movement properties
    private float horizontalSpeed = 4f;
    private float forwardSpeed = 70;
    private Transform target;
    private Vector3 offset;
    private float shipLocation = 10000f;

    //UI
    [SerializeField] private GameObject dolphinGotAwayPanel;
    [SerializeField] private GameObject dolphinIsHitPanel;

    //Class
    private AudioManager audioManager;

    //audioManager properties
    int audioIndex;

    private void Awake()
    {
        target = GameObject.Find("Ship").transform;
    }

    private void Start()
    {
        offset = transform.position - target.transform.position;

        audioManager = FindObjectOfType<AudioManager>();
        
        //determine index of "DolphinNoise"
        audioIndex = audioManager.ReturnAudioIndex("DolphinNoise");
        
        InvokeRepeating("PlayDolphinNoise", 1f, 5f);
    }

    public void DolphinGotAway()
    {
        Debug.Log("GotAway function has been called.");
        dolphinGetsAway = true;
    }

    public void DolphinIsHit()
    {
        Debug.Log("DolphinIsHit function has been called.");
        isDolphinHit = true;

        Invoke("ShowDolphinIsHitPanel", 0.5f);
    }

    private void ShowDolphinIsHitPanel()
    {
        dolphinIsHitPanel.SetActive(true);
    }

    private void FixedUpdate()
    {
        //randomize horizontal speed
        //horizontalSpeed = Random.Range(3f, 5f);

        if(!isDolphinHit && !dolphinGetsAway)
        {
            Vector3 targetPosition = target.transform.position + offset;
            targetPosition.x = Mathf.PingPong(Time.fixedTime * horizontalSpeed, 40) - 20;
            transform.position = targetPosition;
            
            //sync stereo with the position of dolphin
            audioManager.AudioList[audioIndex].source.panStereo = transform.position.x / 20;
        }
        else if(dolphinGetsAway && transform.position.z < 9000f)
        {
            if(shipLocation == 10000f)
            {
                shipLocation = transform.position.z;
            }

            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        }

        if(transform.position.z > shipLocation + 250)
        {
            dolphinGotAwayPanel.SetActive(true);
        }

        if(isDolphinHit == true || dolphinGetsAway == true)
        {
            CancelInvoke("PlayDolphinNoise");
        }
    }

    private void PlayDolphinNoise()
    {
        audioManager.Play("DolphinNoise");
    }
}