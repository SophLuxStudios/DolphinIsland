using UnityEngine;
using UnityEngine.UI;

public class ProgressToggleManager : MonoBehaviour
{
    [SerializeField] private Toggle progressToggle;

    void Awake()
    {
        progressToggle.GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("DisplayProgress", 1) == 1;
    }

    public void ProgressTogglePressed()
    {
        PlayerPrefs.SetInt("DisplayProgress", progressToggle.GetComponent<Toggle>().isOn ? 1 : 0);
    }
}