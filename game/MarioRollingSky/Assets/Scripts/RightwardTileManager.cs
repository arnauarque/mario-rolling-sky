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

public class RightwardTileManager : MonoBehaviour
{

    public float speed = 20f;
    private float initialX;

    // Start is called before the first frame update
    void Start()
    {
        initialX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceTravelled = transform.position.x - initialX;

        if (distanceTravelled < 1f)
        {
            transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
    }
}
