using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            return;
        }
        Debug.Log("Sound is playing :D");
        Debug.Log(s.name);
        Debug.Log(s.volume);
        s.source.Play();
    }

    public void PlayRandomNotes() {
        // Random r = new Random();
        int rInt = UnityEngine.Random.Range(1,7);
        switch (rInt) {
            case 1: {
                Play("Do");
                break;
            }
            case 2: {
                Play("Re");
                break;
            }
            case 3: {
                Play("Mi");
                break;
            }
            case 4: {
                Play("Fa");
                break;
            }
            case 5: {
                Play("Sol");
                break;
            }
            case 6: {
                Play("La");
                break;
            }
            case 7: {
                Play("Si");
                break;
            }
        }
    }

}
