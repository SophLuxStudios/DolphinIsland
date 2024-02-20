using UnityEngine;

public class ControllerSelected : MonoBehaviour
{
    public GameObject touchSelectionCursor;
    public GameObject tiltSelectionCursor;

    private void Awake()
    {
        ControllerCursor(); 
    }

    public void SetContoller(string controller)
    {
        PlayerPrefs.SetString("Controller", controller);
        ControllerCursor();
    }

    private void ControllerCursor()
    {
        if(PlayerPrefs.GetString("Controller", "Touch") == "Touch")
        {
            touchSelectionCursor.SetActive(true);
            tiltSelectionCursor.SetActive(false);           
        }
        else if(PlayerPrefs.GetString("Controller") == "Tilt")
        {
            tiltSelectionCursor.SetActive(true); 
            touchSelectionCursor.SetActive(false);
        }
    }
}