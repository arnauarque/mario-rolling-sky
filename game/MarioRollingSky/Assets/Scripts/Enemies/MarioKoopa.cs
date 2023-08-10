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

public class MarioKoopa : MonoBehaviour
{

    private GameObject ball;
    private Transform koopa;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        koopa = transform.GetChild(0);
        koopa.gameObject.SetActive(false);
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = koopa.position.z - ball.transform.position.z;

        if (distance <= 20f && !active)
        {
            active = true;
            koopa.gameObject.SetActive(true);
        }
        else if (distance <= -3f && active)
        {
            Destroy(gameObject);
        }
    }
}
