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

public class MarioMysteryBlock : MonoBehaviour
{
    public float spawnSpeed = 1f;
    public float detectDistance = 3f;
    public float destroyDistance = 10f;
    public float maxY = 0.2f;
    public bool rotating = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Player");

        float distance = Mathf.Abs(transform.position.z - ball.transform.position.z);

        if ((distance <= detectDistance) && (transform.position.y < maxY))
        {
            if(!rotating)
            {
                rotating = true;
                GetComponent<PerpetualRotate>().enabled = true;
            }
            transform.Translate(0.0f, spawnSpeed * Time.deltaTime, 0.0f, Space.World);
        }
        //Elimina l'objecte si la bola ja ha passat
        else if (distance > destroyDistance && ball.transform.position.z > transform.position.z)
        {
            Destroy(gameObject);
            return;
        }
    }

}