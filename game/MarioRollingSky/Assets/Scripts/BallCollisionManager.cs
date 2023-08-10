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

public class BallCollisionManager : MonoBehaviour
{

    private bool jumping;

    public GameObject deathScreenPrefab;
    private GameObject deathScreen;
    public float minHeight;
    private bool dying = false;

    private Vector3 initialPosition;

    private JumpingTileManager jumpTileManager;

    // Start is called before the first frame update
    void Start()
    {
        jumping = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //En cas que estiguis saltant
        if (jumping)
        {
            //float u = (Mathf.Floor(initialPosition.z+5f)-initialPosition.z)/2;
            //float a = -3 / Mathf.Pow(-u, 2);
            //Debug.Log(a + "     " + u);
            float z = transform.position.z - initialPosition.z;
            if (z < 0) z = 0f;
            else if (z < 4) //(Mathf.Floor(initialPosition.z + 5f) - initialPosition.z)
            {
                //Parabola descrita per f(z) = -0.48 * (z - 2.5)^2 + 3.5
                //float y = a * Mathf.Pow(z - u, 2) + 3.5f;
                float y = -0.5f * Mathf.Pow(z-2f, 2) + 2.5f;
                jumpTileManager.SetY(y);
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
                jumpTileManager.enabled = false;
                jumping = false;
            }
        }

        //Fora dels limits del mapa (esq / dret) -> caus al buit
        else if (transform.position.x < -2.5f || transform.position.x > 2.5f)
        {
            transform.GetComponent<ConstantForce>().force = new Vector3(0f, -50f, 0f);
        }

        if (transform.position.y < minHeight & !dying)
        {
            dying = true;
            //STOP MOVEMENT BALL
            transform.GetComponent<SphereCollider>().enabled = false;
            transform.GetComponent<FrontMovement>().enabled = false;
            transform.GetComponent<BallSideMovement>().enabled = false;
            transform.GetComponent<MeshRenderer>().enabled = false;
            transform.GetComponent<ConstantForce>().enabled = false;
            transform.GetComponent<Rigidbody>().isKinematic = true;

            transform.rotation.eulerAngles.Set(0f, 0f, 0f);
            transform.GetComponent<ParticleSystem>().Play();

            GameObject Camera = GameObject.FindGameObjectWithTag("MainCamera");
            Camera.GetComponent<FrontMovement>().enabled = false;
            Camera.GetComponent<ParticleSystem>().Stop();

            GameObject BackGroundImage = GameObject.FindGameObjectWithTag("BackgroundImage");
            BackGroundImage.GetComponent<FrontMovement>().enabled = false;

            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                GameObject Boss = GameObject.FindGameObjectWithTag("Boss");
                Boss.GetComponent<FrontMovement>().enabled = false;
                Boss.GetComponent<Animator>().enabled = false;
                Boss.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();

                GameObject Cloud = GameObject.FindGameObjectWithTag("Cloud");
                Cloud.GetComponent<FrontMovement>().enabled = false;

                GameObject.FindGameObjectWithTag("Lifebar").SetActive(false);
            }

            //DEATHSCREEN CREATION
            deathScreen = (GameObject)Instantiate(deathScreenPrefab, Camera.transform.position, Camera.transform.rotation);
            deathScreen.GetComponent<deathManager>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string nameObject = collision.gameObject.name;

        if (nameObject == "TransparentTile(Clone)")
        {
            if (!SolidTileHit())
            {
                transform.GetComponent<SphereCollider>().enabled = false;
                transform.GetComponent<ConstantForce>().force = new Vector3(0f, -50f, 0f);
            }
        }

        else if (nameObject == "JumpingMushroomTile(Clone)" | nameObject == "JumpingTile(Clone)" | nameObject == "JumpingTile3(Clone)")
        {
            jumping = true;
            jumpTileManager = collision.gameObject.GetComponent<JumpingTileManager>();
            jumpTileManager.enabled = true;
            jumpTileManager.SetDistanceToBall(transform.position.y - collision.gameObject.transform.position.y);
            jumpTileManager.SetY(collision.gameObject.transform.position.y);

            initialPosition = transform.position;
        }

        else if (nameObject == "MarioForwardTile(Clone)")
        {
            collision.gameObject.GetComponent<ForwardTileManager>().enabled = true;
            FindObjectOfType<AudioManager>().Play("forwardSound");
        }
        else if (nameObject == "2MarioForwardTile(Clone)" | nameObject == "3MarioForwardTile(Clone)" | nameObject == "4MarioForwardTile(Clone)" | nameObject == "5MarioForwardTile(Clone)")
        { 
            collision.gameObject.GetComponent<MultipleForwardTileManager>().enabled = true;
            FindObjectOfType<AudioManager>().Play("forwardSound");
        }
        else if (nameObject == "RightwardTile(Clone)")
        {
            collision.gameObject.GetComponent<RightwardTileManager>().enabled = true;
        }
        else if (nameObject == "MarioFallingTile(Clone)" | nameObject == "cloudTile2(Clone)")
        {
            collision.gameObject.GetComponent<FallingTileManager>().enabled = true;
            FindObjectOfType<AudioManager>().Play("fallingSound");
        }
        else if (nameObject == "MarioBoostTile(Clone)")
        {
            GetComponent<FrontMovement>().SendMessage("Boost",true);

            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<FrontMovement>().SendMessage("Boost", true);
            camera.GetComponent<ParticleSystem>().Play();

            GameObject.FindGameObjectWithTag("BackgroundImage").GetComponent<FrontMovement>().SendMessage("Boost", true);

            AudioManager a = FindObjectOfType<AudioManager>();
            a.GetComponent<FrontMovement>().SendMessage("Boost", true);
            a.Play("boostSound");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "MarioFallingTile(Clone)" | collision.gameObject.name == "cloudTile2(Clone)")
        {
            collision.gameObject.GetComponent<FallingTileManager>().SendMessage("setFalling",true);
        }
    }

    private bool SolidTileHit()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position + Vector3.forward * 0.1f, Vector3.down, 100f);

        bool autoMoveHit = false;

        for (int i = 0; i < hits.Length && !autoMoveHit; ++i)
        {
            if (hits[i].transform.name == "AutoMoveTile(Clone)" || hits[i].transform.name == "MarioForwardTileBase" || hits[i].transform.name == "3MarioForwardTile(Clone)")
            {
                autoMoveHit = true;
            }
        }

        return autoMoveHit;
    }

    private void PrintInitialSituation()
    {
        Debug.Log(
            "ARA COMENÇO EL SALT" +
            "\nPosicio Inicial: " + initialPosition.ToString("F5")

            + "\n\n\n");
    }

    private void PrintLastSituation()
    {
        print("ARA ACABO EL SALT" +
                "\nEND JUMP POSITION: " + transform.position.ToString("F5") +
                "\nDELTA Z: " + (transform.position.z - initialPosition.z).ToString("F5")

                + "\n\n\n");
    }

    private void PrintMediumSituation()
    {
        print("ARA ESTIC EN MIG DEL SALT:" +
            "\nPosicio intermitja: " + transform.position.ToString("F5") +
            "\nHe avançat en Z: " + (transform.position.z - initialPosition.z).ToString("F5")

            + "\n\n\n");
    }
}
