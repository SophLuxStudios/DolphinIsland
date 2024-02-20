using UnityEngine;
using UnityEngine.UI;

public class MuteManager : MonoBehaviour
{
    private bool isMuted;

    private Toggle muteToggle;

    public void Awake()
    {
        muteToggle = gameObject.GetComponent<Toggle>();

        muteToggle.isOn = PlayerPrefs.GetInt("IsMuted") != 1 ;
    }

    private void Start()
    {
        isMuted = PlayerPrefs.GetInt("IsMuted") == 1;
        AudioListener.pause = isMuted;
    }

    public void MutePressed()
    {
        PlayerPrefs.SetInt("IsMuted", muteToggle.isOn == true ? 0 : 1);
        AudioListener.pause = !muteToggle.isOn;
        Debug.Log("IsMuted is: " + !muteToggle.isOn);
    }
}