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

public class FrontMovement : MonoBehaviour
{
    private GameController gameController;
    private Vector3 initialPosition;
    private bool restarted;
    private float frontSpeed;
    private bool isBoosted = false;
    private float timeBoosted = 0f;
    public float boost;
    public float boostTime;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        initialPosition = transform.position;
        restarted = false;
        frontSpeed = gameController.frontSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameController.GameStarted())
        {
            restarted = false;
            transform.Translate(Vector3.forward * frontSpeed * Time.fixedDeltaTime, Space.World);
        }

        else if (!restarted)
        {
            Vector3 translation = initialPosition - transform.position;
            transform.Translate(translation, Space.World);
            restarted = true;
        }

        if (isBoosted)
        {
            timeBoosted = timeBoosted + Time.fixedDeltaTime;
            if (timeBoosted >= boostTime) Boost(false);
        }
        
    }

    void Boost(bool b)
    {
        if (b)
        {
            if (isBoosted)
            {
                timeBoosted = 0f;
            }
            else 
            {
                isBoosted = true;
                frontSpeed = frontSpeed + boost;
            }
        }
        else if (!b && isBoosted)
        {
            isBoosted = false;
            timeBoosted = 0f;
            frontSpeed = frontSpeed - boost;
            if (tag == "MainCamera") GetComponent<ParticleSystem>().Stop();
        }
    }
}
