using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //private properties
    private Transform target;
    private Vector3 offset;

    private const float smoothSpeed = 10f;
    
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
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}