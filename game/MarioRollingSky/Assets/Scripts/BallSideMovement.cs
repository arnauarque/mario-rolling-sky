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

public class BallSideMovement : MonoBehaviour
{

    public bool mouseMovement;

    private GameController gameController;
    private float lateralStep;
    private bool restarted = false;

    private float posAnt;
    private float posNew;

    // Start is called before the first frame update
    void Start()
    {
        mouseMovement = false;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        lateralStep = gameController.lateralStep;

        posAnt = Input.mousePosition.x;
        posNew = posAnt;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        posAnt = posNew;
        posNew = Input.mousePosition.x;

        if (gameController.GameStarted())
        {
            restarted = false;

            if (mouseMovement && Input.GetMouseButton(0))
            {
                float distance = ConvertPixelsToUnits(posNew - posAnt);
                transform.Translate(distance, 0f, 0f);
            }

            else if (!mouseMovement) {
                //Moure's a l'esquerra
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(Vector3.left * lateralStep);
                }
                //Moure's a la dreta
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(Vector3.right * lateralStep);
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.up * lateralStep);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            }
        }

        else if (!restarted)
        {
            transform.GetComponent<ConstantForce>().force = new Vector3(0f, 0f, 0f);
            transform.GetComponent<SphereCollider>().enabled = true;
            restarted = true;
        }
    }

    private float ConvertPixelsToUnits(float pixels)
    {
        float units = pixels / 175f;
        return units;
    }

    public void setMouseMovement(bool b)
    {
        mouseMovement = b;
    }

    public void RestartMousePosition()
    {
        posAnt = Input.mousePosition.x;
        posNew = posAnt;
    }
}
