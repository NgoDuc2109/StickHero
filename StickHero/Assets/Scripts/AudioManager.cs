using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sound
{
    public AudioMixerGroup audioMixer;
    private AudioSource source;
    public string clipName;
    public AudioClip clip;
    [Range(0, 1)]
    public float volume, pitch;
    public bool loop = false;
    public bool playOnAwake = false;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.pitch = pitch;
        source.volume = volume;
        source.playOnAwake = playOnAwake;
        source.loop = loop;
        source.outputAudioMixerGroup = audioMixer;
    }

    public void Play()
    {
        source.Play();
    }

    public void MuteOff()
    {
        source.mute = false;
    }

    public void MuteOn()
    {
        source.mute = true;
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private List<Sound> sound;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < sound.Count; i++)
        {
            GameObject _go = new GameObject("Sound" + i + "_" + sound[i].clipName);
            _go.transform.SetParent(gameObject.transform);
            sound[i].SetSource(_go.AddComponent<AudioSource>());
        }

        PlaySound(Const.Audio.BACKGROUND);
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sound.Count; i++)
        {
            if (sound[i].clipName == _name)
            {
                sound[i].Play();
                return;
            }
        }
    }

    public void MuteOn_All()
    {
        for (int i = 0; i < sound.Count; i++)
        {
            sound[i].MuteOn();

        }
    }

    public void MuteOff_All()
    {
        for (int i = 0; i < sound.Count; i++)
        {
            sound[i].MuteOff();

        }
    }
}
