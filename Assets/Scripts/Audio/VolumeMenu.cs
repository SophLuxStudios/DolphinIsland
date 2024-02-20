using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeMenu : MonoBehaviour
{
    public AudioMixer effectMixer;
    public Slider effectSlider;

    public AudioMixer musicMixer;
    public Slider musicSlider;

    private void Awake()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicSliderValue", .6f);
 
        effectSlider.value = PlayerPrefs.GetFloat("effectSliderValue", .6f);
    }

    public void SetMusicVolume(float musicVolume)
    {
        PlayerPrefs.SetFloat("musicSliderValue", musicVolume);
        PlayerPrefs.SetFloat("musicVolValue", Mathf.Log10(musicVolume) * 20f);
        musicMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20f);
    }
    
    public void SetEffectVolume(float effectVolume)
    {
        PlayerPrefs.SetFloat("effectSliderValue", effectVolume);
        PlayerPrefs.SetFloat("effectVolValue", Mathf.Log10(effectVolume) * 20f);
        effectMixer.SetFloat("effectVolume", Mathf.Log10(effectVolume) * 20f);
    }
}