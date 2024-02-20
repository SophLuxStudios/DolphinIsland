using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipPosition : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;

    private bool inStoryMode;

    void Start()
    {
        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("StoryMode"))
        {
            inStoryMode = true;

            if(PlayerPrefs.GetInt("DisplayProgress", 1) == 1)
            {
                progressSlider.gameObject.SetActive(true);
            }
            else progressSlider.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(inStoryMode)
        {
            progressSlider.value = transform.position.z;
        }
    }
}