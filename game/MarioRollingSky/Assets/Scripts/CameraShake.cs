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

public class CameraShake : MonoBehaviour
{

    public float power = 1f;
    public float duration = 1f;
    public float slowDownFactor = 0.5f;

    private Vector3 initialPosition;
    private float initialDuration;

    public bool shake; 

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
        initialDuration = duration;
        shake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shake)
        {
            if (duration > 0f)
            {
                Vector2 randomMovement = Random.insideUnitCircle;
                transform.localPosition = new Vector3(initialPosition.x + randomMovement.x * power, initialPosition.y + randomMovement.y * power, transform.localPosition.z);
                duration -= Time.deltaTime * slowDownFactor;
            }
            else
            {
                shake = false;
                duration = initialDuration;
                transform.localPosition = new Vector3(initialPosition.x, initialPosition.y, transform.localPosition.z);
            }
        }
    }

    public void Shake()
    {
        initialPosition = transform.localPosition;
        shake = true;
    }

    public void SetDuration(float d)
    {
        initialDuration = d;
        duration = d;
    }

    public void SetPower(float p)
    {
        power = p;
    }
}
