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

public class MarioHammer : MonoBehaviour
{

    private GameObject ball;
    public float activateDistance = 3.5f;
    public float destroyDistance = 10f;

    bool playedSound = false;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = transform.position.z - ball.transform.position.z;

        if (distance <= activateDistance)
        {
            if (!playedSound)
            {
                playedSound = true;
                FindObjectOfType<AudioManager>().Play("hammerSound");
            }
            transform.GetComponent<Animator>().enabled = true;
        }
        //Elimina l'objecte si la bola ja ha passat
        else if (distance > destroyDistance && ball.transform.position.z > transform.position.z)
        {
            Destroy(gameObject);
            return;
        }
    }
}
