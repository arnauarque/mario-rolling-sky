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

public class MarioChomp : MonoBehaviour
{
    // Start is called before the first frame update
    public float attackDistance = 10f;
    public float animationSpeed = 2f;
    public float destroyDistance = 10f;
    private bool playedSound = false;

    void Start()
    {
        transform.GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Calcula la distancia i fa l'spawn
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        UnityEngine.Transform b = playerObject.GetComponent<Transform>();
        Vector2 ballPosition = new Vector2(b.transform.position.z, b.transform.position.x);
        Vector2 chompPosition = new Vector2(transform.position.z, transform.position.x);

        float distance = Vector2.Distance(ballPosition, chompPosition);
        if (distance < attackDistance)//transform.localScale.z<0.1
        {

            if (!playedSound)
            {
                playedSound = true;
                FindObjectOfType<AudioManager>().Play("chompSound");
            }
            transform.GetComponent<Animator>().speed = animationSpeed;
            transform.GetComponent<Animator>().enabled = true;

            //transform.localScale += new Vector3(0.0f, 0.0f, spawnSpeed * Time.deltaTime);

        }
        //Elimina l'objecte si la bola ja ha passat
        else if (distance > destroyDistance && b.position.z > transform.position.z)
        {
            Destroy(gameObject);
            return;
        }
    }
}
