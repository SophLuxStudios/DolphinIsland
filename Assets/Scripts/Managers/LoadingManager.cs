using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Text LoadingProgressText;

    private bool isRunning = false;

    void Start()
    {
        if(!isRunning)
        {
            StartCoroutine(LoadAsynchronously(PlayerPrefs.GetString("SceneName")));
        }
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        isRunning = true;

        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneName);

        while(!loadingOperation.isDone)
        {   
            float loadingProgress = Mathf.Clamp01(loadingOperation.progress / 0.9f);

            LoadingProgressText.text = (loadingProgress * 100f).ToString("f0") + "%";

            yield return null;
        }
    }
}