// -------------------------------------------------------------------------- //
// -- MarioRollingSky                                                      -- //
// -------------------------------------------------------------------------- //
// -- Autors: Arnau Arqué and Daniel Esquina                               -- //
// -------------------------------------------------------------------------- //
// -- Projecte part de l'assignatura de Videojocs del Grau en Enginyeria   -- //
// -- Informàtica (Facultat d'Informàtica de Barcelona, Universitat        -- //
// -- Politècnica de Catalunya)                                            -- //
// -------------------------------------------------------------------------- //

using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public List<Sound> sounds;
    public static AudioManager instance;



    public int coin1 = 0;
    public int coin2 = 0;
    public int coin3 = 0;

    public int coinActual;

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        } 

        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        Play("themeLevel0");
    }

    public void Play(string name)
    {
        List<Sound> aux =sounds.FindAll(sound => sound.name == name);
        bool found = false;
        if (aux.Count>0)
        {
            foreach (Sound s in aux)
            {
                if (!s.source.isPlaying & !found)
                {
                    found = true;
                    s.source.Play();
                }
            }
            if(!found)
            {
                Sound s2 = new Sound();
                s2.name = name;
                s2.source = gameObject.AddComponent<AudioSource>();
                s2.source.clip = aux[0].clip;
                s2.source.volume = aux[0].volume;
                s2.source.pitch = aux[0].pitch;
                s2.source.loop = aux[0].loop;
                s2.source.Play();
                sounds.Add(s2);
            }
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        } 
      
    }
    public void Pause(string name)
    {
        List<Sound> aux = sounds.FindAll(sound => sound.name == name);
        aux[0].source.Pause();
    }

    public void unPause(string name)
    {
        List<Sound> aux = sounds.FindAll(sound => sound.name == name);
        aux[0].source.UnPause();
    }
    public void StopAllSounds()
    {
        foreach(Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    public void restartCoins()
    {
        coinActual = 0;
    }

    public void addCoin()
    {
        coinActual += 1;
    }

    public void updateCoins()
    {
        if(SceneManager.GetActiveScene().buildIndex==1)
        {
            coin1 = Mathf.Max(coin1, coinActual);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            coin2 = Mathf.Max(coin2, coinActual);

        }
        else if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            coin3 = Mathf.Max(coin3, coinActual);

        }
    }

    public int getCoin(int level)
    {
        if (level==1)
        {
            return coin1;
        }
        else if (level == 2)
        {
            return coin2;

        }
        else if (level == 3)
        {
            return coin3;

        }
        return 23;
    }

    public int getActualCoins()
    {
        return coinActual;
    }

    public void silenceRace()
    {
        List<Sound> aux = sounds.FindAll(sound => sound.name == "raceSound");
        aux[0].source.volume = 0f;
    }

    public void unSilenceRace()
    {
        List<Sound> aux = sounds.FindAll(sound => sound.name == "raceSound");
        aux[0].source.volume = 0.2f;
    }
}
