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

public class BallCollisionEnemyManager : MonoBehaviour
{
    public GameObject deathScreenPrefab;
    private GameObject deathScreen;
    private bool dying = false;

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss") && !dying)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            Die();
        }
    }

    public void Die()
    {
        dying = true;
        //Destroy(transform.gameObject);
        //STOP MOVEMENT
        transform.GetComponent<SphereCollider>().enabled = false;
        transform.GetComponent<FrontMovement>().enabled = false;
        transform.GetComponent<BallSideMovement>().enabled = false;
        transform.GetComponent<MeshRenderer>().enabled = false;
        transform.GetComponent<ConstantForce>().enabled = false;
        transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
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
