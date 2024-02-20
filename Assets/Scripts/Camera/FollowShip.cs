using UnityEngine;

public class FollowShip : MonoBehaviour
{
    //private properties
    private Transform target;
    private Vector3 offset;

    private void Awake()
    {
        target = GameObject.Find("Ship").transform;
    }

    private void Start()
    {
        offset = transform.position - target.transform.position;
    }

    private void FixedUpdate()
    {       
        Vector3 targetPosition = target.transform.position + offset;
        targetPosition.x = 0;
        transform.position = targetPosition;
    }
}