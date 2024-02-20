using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    //Serialized components
    [SerializeField] private ParticleSystem woodSplash;

    //make class public
    public static ShipHealth shipHealth;

    //used class
    [SerializeField] private InGameMenu inGameMenu;

    //public properties
    public bool alive = true;
    public bool shipDiedOnce;

    //private properties
    private Vector3 splashPosition;
    private Rigidbody rbBoat;
    [SerializeField] private GameObject continueButton;

    void Start()
    {
        shipHealth = this;
        rbBoat = this.gameObject.transform.GetChild(0).GetComponent<Rigidbody>();
    }

    public void Die()
    {
        Debug.Log("ship died");

        if(shipDiedOnce)
        {
            continueButton.SetActive(false);
        }

        alive = false;
        shipDiedOnce = true;
        DeadPanel();
        FindObjectOfType<AudioManager>().Play("ShipImpact");
        MakeBoatKinematic();
        
    }

    private void DeadPanel()
    {
        inGameMenu.IsShipDead(!alive);
    }

    public void Reborn()
    {
        this.gameObject.transform.position = 
        new Vector3
        (
            0, 
            this.gameObject.transform.position.y,
            this.gameObject.transform.position.z
        );

        alive = true;
        DeadPanel();
        Debug.Log("Ship is back alive");

        Invoke("MakeBoatNotKinematic", 0.5f);
    }

    private void MakeBoatKinematic()
    {
        rbBoat.isKinematic = true;
    }

    private void MakeBoatNotKinematic()
    {
        rbBoat.isKinematic = false;
    }
}