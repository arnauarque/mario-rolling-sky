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

public class AutoMoveTileManager : MonoBehaviour
{

    private GameController gameController;
    private float lateralSpeed;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        lateralSpeed = gameController.frontSpeed / 2f;
        lateralSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GameStarted())
        {
            float distance = lateralSpeed * Time.deltaTime;
            float Xini = transform.position.x;
            float Xend = transform.position.x + distance;

            if (Xend > 2)
            {
                distance = -(Xini + Xend - 4);
                lateralSpeed *= -1;
            }

            else if (Xend < -2)
            {
                distance = -(Xini + Xend + 4);
                lateralSpeed *= -1;
            }

            transform.Translate(distance, 0f, 0f, Space.World);
        }
    }
}
