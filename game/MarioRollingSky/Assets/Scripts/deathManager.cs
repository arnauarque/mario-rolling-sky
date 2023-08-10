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
using UnityEngine.SceneManagement;
using UnityEngine;

public class deathManager : MonoBehaviour
{
    public GameObject deathMenuPrefab;
    private bool createdMenu = false;
    public float dyingTime = 3f;
    private Vector3 scaleVector;
    [Range(0f, 1f)]
    public float minScale = 0.05f;
    [Range(0f, 1f)]
    public float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("PauseIcon"));
        //STOP MUSIC
        AudioManager a = FindObjectOfType<AudioManager>();
        a.StopAllSounds();
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            a.Play("BowserKillLaugh");
        }
        else
        {
            a.Play("deathSound");
        }
        a.updateCoins();

        scaleVector = new Vector3(Time.deltaTime * speed, Time.deltaTime * speed, 0);
        transform.Translate(Vector3.forward, Space.Self);

    }

    // Update is called once per frame
    void Update()
    {
        if (dyingTime > 0) dyingTime = dyingTime - Time.deltaTime;
        else
        {
            if (!createdMenu)
            {
                GameObject deathMenu = (GameObject)Instantiate(deathMenuPrefab, transform.position, transform.rotation);
                createdMenu = true;
            }
        }
        if (transform.localScale.x > minScale) transform.localScale = transform.localScale - scaleVector;
        
    }
}
