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
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndJump : MonoBehaviour
{

    private GameObject endFlag;
    private Vector3 initialPosition;

    private float dx;
    private float dz;

    private Vector3 newPosition;

    private bool goDown;

    public float downVelocity = 0.5f;
    public float rotationVelocity = 60f;

    // Start is called before the first frame update
    void Start()
    {
        endFlag = GameObject.FindGameObjectWithTag("EndFlag");
        initialPosition = transform.position;

        dx = 0.45f - initialPosition.x;
        dz = endFlag.transform.position.z - initialPosition.z;

        goDown = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!goDown)
        {
            newPosition.z = transform.position.z;
            float z = transform.position.z - initialPosition.z;

            float deltaX = dx * z / dz;
            float x = initialPosition.x + deltaX;
            newPosition.x = x;
            //transform.position = new Vector3(x, transform.position.y, transform.position.z);

            if (z < 0) z = 0f;

            if (z < 2f)
            {
                float y = -0.5f * Mathf.Pow(z - 2f, 2) + 2.5f;
                newPosition.y = y;
                //transform.position = new Vector3(transform.position.x, y, transform.position.z);
                //Time.timeScale = 0f;
            }
            else if (transform.position.z < endFlag.transform.position.z)
            {
                newPosition.y = transform.position.y + 2 * Time.deltaTime;
                //transform.Translate(Vector3.forward * Time.deltaTime, Space.World);
            }
            else if (transform.position.z >= endFlag.transform.position.z)
            {

                //Parem el FRONT MOVEMENT
                GetComponent<BallSideMovement>().enabled = false;
                GetComponent<FrontMovement>().enabled = false;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FrontMovement>().enabled = false;
                GameObject.FindGameObjectWithTag("BackgroundImage").GetComponent<FrontMovement>().enabled = false;

                if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    GameObject.FindGameObjectWithTag("Boss").GetComponent<FrontMovement>().enabled = false;
                    GameObject.FindGameObjectWithTag("Boss").GetComponent<BowserManager>().SetLife(1);
                    GameObject.FindGameObjectWithTag("Boss").GetComponent<BowserManager>().Impact(-1);
                    GameObject.FindGameObjectWithTag("Cloud").GetComponent<FrontMovement>().enabled = false;
                }

                // x = 0.45, y = indiferent, z = EndPlane.z
                newPosition = new Vector3(0.45f, transform.position.y, endFlag.transform.position.z);
                //transform.position = new Vector3(0.45f, transform.position.y, endFlag.transform.position.z);

                //STOP MUSIC
                AudioManager a = FindObjectOfType<AudioManager>();
                a.StopAllSounds();
                a.Play("flagpoleSound");

                goDown = true;
            }

            transform.position = newPosition;
        }

        else
        {
            transform.RotateAround(endFlag.transform.position, Vector3.down, rotationVelocity * Time.deltaTime);
            transform.Translate(Vector3.down * downVelocity);

            if (transform.position.y <= 0.65f)
            {
                transform.position = new Vector3(transform.position.x, 0.65f, transform.position.z);
                AudioManager a = FindObjectOfType<AudioManager>();
                a.updateCoins();
                GameObject.FindGameObjectWithTag("GameController").GetComponent<MainMenu>().PlayGame(0);
                enabled = false;
            }
        }
    }
}
