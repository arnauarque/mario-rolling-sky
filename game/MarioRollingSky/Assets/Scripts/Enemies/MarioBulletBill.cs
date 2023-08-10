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

public class MarioBulletBill : MonoBehaviour
{

    public float activateDistance = 5.5f;

    private GameObject ball;
    private Animator anim;
    private bool attackIsDone;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponent<Animator>();
        ball = GameObject.FindGameObjectWithTag("Player");
        attackIsDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = transform.position.z - ball.transform.position.z;

        //Arnau: No cal, pero ho deixo posat per si m'es util saber com s'accedeix als noms dels States mes endavant
        //bool nameCondition = anim.GetCurrentAnimatorStateInfo(0).IsName("Breathing");

        if (distance <= activateDistance && !attackIsDone)
        {
            anim.enabled = true;
            attackIsDone = true;
        }

        else if (distance <= -3f)
        {
            Destroy(gameObject);
        }
    }

    public void ActivateBulletBillFireEvent()
    {
        FindObjectOfType<AudioManager>().Play("BulletBillSound");
        GetComponentInChildren<ParticleSystem>().Play();
    }

    public void StopBulletBillFireEvent()
    {
        GetComponentInChildren<ParticleSystem>().Stop();
    }
}
