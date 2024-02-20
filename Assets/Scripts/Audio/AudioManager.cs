using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer effectMixer;
    public AudioMixer musicMixer;

    public AudioProperties[] AudioList;

    [Serializable]
    public struct AudioProperties
    {
        public string name;

        public AudioClip clip;

        [Range(0.0001f,1f)] 
        public float volume;

        [Range(.1f,3f)]
        public float pitch;

        [Range(-1f,1f)]
        public float stereoPan;

        public bool loop;

        public AudioMixerGroup Mixer;

        [HideInInspector]
        public AudioSource source;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

        for (var i = 0; i < AudioList.Length; i++)
        {
            AudioList[i].source = gameObject.AddComponent<AudioSource>();
            AudioList[i].source.clip = AudioList[i].clip;
            AudioList[i].source.volume = AudioList[i].volume;
            AudioList[i].source.pitch = AudioList[i].pitch;
            AudioList[i].source.panStereo = AudioList[i].stereoPan;
            AudioList[i].source.loop = AudioList[i].loop;
            AudioList[i].source.outputAudioMixerGroup = AudioList[i].Mixer;
        }
    }

    void Start()
    {
        //set mixer values as user's values on start
        musicMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolValue"));
        effectMixer.SetFloat("effectVolume", PlayerPrefs.GetFloat("effectVolValue"));
        
        Play("DrunkenSailor");
        Play("Tropical");
    }

    public void Play(string name)
    {
        for (var i = 0; i < AudioList.Length; i++)
        {
            if (name == AudioList[i].name)
            {
                AudioList[i].source.Play();
            }
        }
    }

    //To change properties from other class'
    public int ReturnAudioIndex(string name)
    {
        int audioIndex = new int();

        for (var i = 0; i < AudioList.Length; i++)
        {
            if (name == AudioList[i].name)
            {
                audioIndex = i;
            }
        }

        return audioIndex;
    }
}