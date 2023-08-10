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

public class ForwardTileManager : MonoBehaviour
{

    private GameController gameController;
    private GameObject ball;
    private float initialZ;
    private float frontSpeed;

    // Start is called before the first frame update
    void Start()
    {
        initialZ = transform.position.z;
        ball = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        frontSpeed = gameController.frontSpeed;

        transform.GetChild(3).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceTravelled = transform.position.z - initialZ;

        if (distanceTravelled < 1f)
        {
            transform.Translate(0.0f, 0.0f, frontSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, initialZ + 1f);
        }

        if (transform.position.z - ball.transform.position.z <= -3f)
        {
            Destroy(gameObject);
        }
    }
}
