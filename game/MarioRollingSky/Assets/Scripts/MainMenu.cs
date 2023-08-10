// -------------------------------------------------------------------------- //
// -- MarioRollingSky                                                      -- //
// -------------------------------------------------------------------------- //
// -- Autors: Arnau Arqué and Daniel Esquina                               -- //
// -------------------------------------------------------------------------- //
// -- Projecte part de l'assignatura de Videojocs del Grau en Enginyeria   -- //
// -- Informàtica (Facultat d'Informàtica de Barcelona, Universitat        -- //
// -- Politècnica de Catalunya)                                            -- //
// -------------------------------------------------------------------------- //

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame (int level)
    {
        Time.timeScale = 1f;
        AudioManager a = FindObjectOfType<AudioManager>();
        a.StopAllSounds();
        a.restartCoins();
        if(level!=0) FindObjectOfType<AudioManager>().Play("menuTheme");
        else FindObjectOfType<AudioManager>().Play("themeLevel" + level);
        if (level != 3) a.silenceRace();
        else a.unSilenceRace();
        SceneManager.LoadScene(level);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        AudioManager a = FindObjectOfType<AudioManager>();
        a.StopAllSounds();
        a.restartCoins();
        a.Play("menuTheme");
        if (SceneManager.GetActiveScene().buildIndex != 3) a.silenceRace();
        else a.unSilenceRace();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            AudioManager a = FindObjectOfType<AudioManager>();
            transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "x   " + a.getCoin(1) + " / 10";
            transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "x   " + a.getCoin(2) + " / 10";
            transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "x   " + a.getCoin(3) + " / 10";
        }
    }

    
}
