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

public class FallingBasicTileManager : MonoBehaviour
{
    public float acc = -9.8f; // ACCELERACIÓ NEGATIVA !!!
    public float activateDistance = 5.5f;

    private GameObject ball;
    private float deltaTemps;
    private float y0;
    private float deltaY;
    private float newY;

    private bool stopFalling;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");

        deltaTemps = 0f;
        y0 = transform.position.y;
        stopFalling = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = transform.position.z - ball.transform.position.z;

        if (distance <= activateDistance & transform.position.y > 0f & !stopFalling)
        {
            deltaTemps += Time.deltaTime;

            newY = y0 + (0.5f * acc * Mathf.Pow(deltaTemps, 2));

            deltaY = transform.position.y - newY;

            if (transform.position.y - deltaY < 0f)
            {
                deltaY = transform.position.y;
                stopFalling = true;
            }

            transform.Translate(Vector3.down * deltaY, Space.World);
        }
    }
}
