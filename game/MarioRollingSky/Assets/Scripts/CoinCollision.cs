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

public class CoinCollision : MonoBehaviour
{

    public GameObject part;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            GameObject aux = (GameObject)Instantiate(part, transform.position, other.transform.rotation);
            aux.transform.Rotate(-90f, 0f, 0f);
            //aux.transform.parent = other.transform;
            FindObjectOfType<AudioManager>().Play("coinSound");
            FindObjectOfType<AudioManager>().addCoin();
            Destroy(gameObject);
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.GetChild(0).GetComponent<ParticleSystem>().Play();

            transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }*/
}
