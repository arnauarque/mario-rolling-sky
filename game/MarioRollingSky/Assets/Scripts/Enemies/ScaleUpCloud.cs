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

public class ScaleUpCloud : MonoBehaviour
{

    public float scaleFactor = 1f;
    private float timeBeforeScaleUp;

    private float deltaTemps;
    private float acc;

    private void Start()
    {
        timeBeforeScaleUp = 2f;
        deltaTemps = 0f;
        acc = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeScaleUp -= Time.deltaTime;

        if (timeBeforeScaleUp <= 0f)
        {
            if (transform.localScale.x < 0.2f)
            {
                deltaTemps += Time.deltaTime;

                float f = 0.5f * acc * Mathf.Pow(deltaTemps, 2);

                //float f = Time.deltaTime * scaleFactor;
                transform.localScale = new Vector3(f, f, f);
            }

            else
            {
                transform.localScale.Set(0.2f, 0.2f, 0.2f);
                enabled = false;
            }
        }
    }
}
