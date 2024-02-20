using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseMenu : MonoBehaviour
{
    private bool isRunning = false;

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadStoryMode()
    {
        PlayerPrefs.SetString("SceneName", "StoryMode");

        if(!isRunning)
        {
            StartCoroutine(LoadAsynchronously("LoadingScene"));
        }
        //SceneManager.LoadSceneAsync("LoadingScene");
        //SceneManager.LoadScene("LoadingScene");
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