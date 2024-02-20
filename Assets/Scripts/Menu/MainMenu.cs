using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool isRunning = false;

    //if it is the first time playin dont ask where to
    public void StoryModeStartFrom()
    {
        if(PlayerPrefs.GetInt("ShipLocatedAt") == 0)
        {
            PlayStoryMode("StoryBase");
        }
    }

    public void PlayStoryMode(string location)
    {
        PlayerPrefs.SetString("SceneName", location);

        if(!isRunning)
        {
            StartCoroutine(LoadAsynchronously("LoadingScene"));
        }
    }

    public void PlayEndlessMode()
    {
        PlayerPrefs.SetString("SceneName", "EndlessRun");

        if(!isRunning)
        {
            StartCoroutine(LoadAsynchronously("LoadingScene"));
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        isRunning = true;

        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneName);

        while(!loadingOperation.isDone)
        {
            yield return null;
        }
    }
}