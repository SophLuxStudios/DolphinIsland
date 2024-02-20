using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerOptions : MonoBehaviour
{
    public GameObject leftButton;
    public GameObject rightButton;

    private void Awake()
    {
        //if there isn't a selected controller select touch by default
        if(string.IsNullOrEmpty(PlayerPrefs.GetString("Controller")))
        {
            PlayerPrefs.SetString("Controller", "Touch");
        }
    }

    private void Start()
    {
        //activate buttons if touch controller is selected, deactivate otherwise
        if(PlayerPrefs.GetString("Controller") == "Touch")
        {
            //Debug.Log("Touch.");
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("EndlessRun") ||
               SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("Menu"))
            {
                leftButton.SetActive(true);
                rightButton.SetActive(true);
            }
            else
            {
                leftButton.SetActive(false);
                rightButton.SetActive(false);
            }
        }
        else if(PlayerPrefs.GetString("Controller") == "Tilt")
        {
            leftButton.SetActive(false);
            rightButton.SetActive(false);
        }
    }

    //Android Input options
    public void ControllerType(string controllerOption)
    {
        PlayerPrefs.SetString("Controller", controllerOption);
    }
}