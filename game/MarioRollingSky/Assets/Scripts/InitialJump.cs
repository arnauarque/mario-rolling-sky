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

public class InitialJump : MonoBehaviour
{

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float z = transform.position.z - initialPosition.z;
        if (z < 0) z = 0f;
        else if (z < 4)
        {
            float y = -0.5f * Mathf.Pow(z - 2f, 2) + 2.5f;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
            enabled = false;
        }
    }
}
