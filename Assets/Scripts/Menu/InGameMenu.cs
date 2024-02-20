using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    //Serialized UIs
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;

    //used scripts
    private GameObject ship;
    private ShipHealth shipHealth;

    private void Awake()
    {
        ship = GameObject.FindGameObjectWithTag("Ship");
        shipHealth = ship.GetComponent<ShipHealth>();

        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("StoryMode"))
        {
            Time.timeScale = 0.0f;
        }
    }

    public void IsShipDead(bool isShipDead)
    {
        ControllerButtonsActivation(!isShipDead);

        deadPanel.SetActive(isShipDead);
    }

    private void ControllerButtonsActivation(bool activation)
    {
        leftButton.SetActive(activation);
        rightButton.SetActive(activation);
    }

    public void PauseGame()
    {
        ControllerButtonsActivation(false);
        Time.timeScale = 0.0f;
    }

    public void ContinueGame()
    {
        ControllerButtonsActivation(true);
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenQuestList()
    {
        if(ship.transform.position.z < 7550)
        {
            PauseGame();
        }
    }

    public void CloseQuestList()
    {
        if(ship.transform.position.z < 7550)
        {
            ContinueGame();
        }
    }

    public void GoToMenu()
    {
        Time.timeScale = 1.0f;
        
        //if in story mode return to base
        /*if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("StoryMode"))
        {
            SceneManager.LoadScene("StoryBase");
        }
        else
        {*/
            SceneManager.LoadScene("Menu");
        //}
    }
}