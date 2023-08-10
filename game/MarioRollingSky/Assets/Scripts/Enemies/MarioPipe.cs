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

public class MarioPipe : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnSpeed = 1f;
    public float detectDistance = 10f;
    public float destroyDistance = 10f;
    public float maxY = 0.2f;
    private bool playedSound = false;
    private bool hasSound = false;

    void Start()
    {
        transform.Translate(0.0f, -1.0f, 0.0f, Space.World);//y=0.0f
        //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {

        //Calcula la distancia i fa l'spawn
        GameObject ball = GameObject.FindGameObjectWithTag("Player");

        float distance = Mathf.Abs(transform.position.z - ball.transform.position.z);

        if ((distance < detectDistance) && (transform.position.y < maxY))//transform.localScale.z<0.1
        {
            if (!playedSound && hasSound)
            {
                playedSound = true;
                FindObjectOfType<AudioManager>().Play("pipeSound");
            }
            transform.Translate(0.0f, spawnSpeed * Time.deltaTime, 0.0f, Space.World);
            //transform.localScale += new Vector3(0.0f, 0.0f, spawnSpeed * Time.deltaTime);

        }
        //Elimina l'objecte si la bola ja ha passat
        else if(distance > destroyDistance && ball.transform.position.z>transform.position.z)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SetHasSound(bool b)
    {
        hasSound = b;
    }
}
