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

public class MarioBrickBlock : MonoBehaviour
{
    public float speed = 1f;
    public float detectDistance = 3f;
    public float minY = 0.2f;

    private GameObject ball;
    private bool active; 

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = transform.position.z - ball.transform.position.z;

        if (distance <= detectDistance && active)
        {
            if (transform.position.y > minY)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, minY, transform.position.z);
                active = false;
            }
        }

        else if (distance <= -3f && !active)
        {
            //Destroy(gameObject);
        }
    }
}
