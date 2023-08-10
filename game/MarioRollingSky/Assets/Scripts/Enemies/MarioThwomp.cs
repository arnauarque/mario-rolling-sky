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

public class MarioThwomp : MonoBehaviour
{
    public float acc = -9.8f; // ACCELERACIÓ NEGATIVA !!!
    public float activateDistance = 5.5f;

    private GameObject ball;
    private float deltaTemps;
    private float y0;
    private float deltaY;
    private float newY;

    private CameraShake cs;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");

        cs = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<CameraShake>();

        deltaTemps = 0f;
        y0 = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = transform.position.z - ball.transform.position.z;

        if (distance <= activateDistance & transform.position.y > 1.035f)
        {
            deltaTemps += Time.deltaTime;

            newY = y0 + (0.5f * acc * Mathf.Pow(deltaTemps, 2));

            deltaY = transform.position.y - newY;

            if (transform.position.y - deltaY < 1.035f)
            {
                deltaY = transform.position.y - 1.035f;
                cs.SetDuration(0.5f);
                cs.SetPower(0.05f);
                cs.Shake();
                FindObjectOfType<AudioManager>().Play("ThwompSound");
            }

            transform.Translate(Vector3.down * deltaY, Space.World);
        }
    }
}
