using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    #region pog
    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    [SerializeField] AudioClip[] soundEffects;



    [SerializeField] private List<AudioSource> audioSource = new List<AudioSource>();
    private void Start()
    {
        audioSource.Add(GetComponent<AudioSource>());
    }
    public void PlaySong(string name)
    {
        switch (name)
        {
            case "fireball":
                PlaySong(0);
                break;
            case "iceball":
                PlaySong(1);
                break;
        }
    }

    public void PlaySong(int index)
    {
        foreach (AudioSource source in audioSource)
        {
            if(!source.isPlaying)
            source.clip = soundEffects[index];
            source.Play();
            return;
        }
       
        AudioSource source2 = CreateNewAudioSource();
        source2.clip = soundEffects[index];
        source2.Play();
    }

    private AudioSource CreateNewAudioSource()
    {
        GameObject GO = new GameObject();
        GO.name = "AudioSource";
        GO.transform.parent = this.transform;
        AudioSource source = GO.AddComponent<AudioSource>();
        audioSource.Add(source);

        return source;
    }
}
