using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisionDetector : MonoBehaviour
{
    [SerializeField] private ShipHealth shipHealth;
    [SerializeField] private ParticleSystem woodSplash;

    void Start()
    {
        shipHealth = gameObject.GetComponentInParent<ShipHealth>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Instantiate ParticleSystem prefab at collision point
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(woodSplash, pos, rot);

        Debug.Log("CollisionDetected. with " + collision.gameObject);
        shipHealth.Die();
    }
}