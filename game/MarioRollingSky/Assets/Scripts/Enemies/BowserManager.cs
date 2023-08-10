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
using UnityEngine.UI;
using UnityEngine;

public class BowserManager : MonoBehaviour
{
    private GameObject gameController;

    private AudioManager a;

    private Animator anim;
    private float delay;

    private int life;

    private bool initialJump;
    private bool roar; 
    private bool jumpUp;
    private bool attack;
    private bool damage;
    private bool getUp;
    private bool die;
    private bool fireCollider;

    private bool startCountdown;

    public GameObject cloud;
    private Transform fireBreath;

    public Slider lifebar;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0f, 0f, 0f);

        gameController = GameObject.FindGameObjectWithTag("GameController");
        a = FindObjectOfType<AudioManager>();
        anim = GetComponent<Animator>();
        anim.enabled = false;
        fireBreath = transform.GetChild(0);

        delay = 0f;
        life = 6;

        initialJump = false;
        roar = false;
        jumpUp = false;
        attack = false;
        damage = false;
        getUp = false;
        die = false;
        fireCollider = false;

        //lifebar = GameObject.FindGameObjectWithTag("Lifebar");
        lifebar.maxValue = life;
        lifebar.value = lifebar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {

        if (delay > 0f) delay -= Time.deltaTime;

        else if (delay <= 0f)
        {
            if (initialJump)
            {
                transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

                anim.enabled = true;
                a.Play("bowserStomp");

                Roar(3.542f + 1f);

                initialJump = false;
            }
            else if (roar)
            {
                anim.SetTrigger("Roar");

                JumpUp(6.875f + 2f);

                roar = false;
            }
            else if (jumpUp)
            {
                Instantiate(cloud, cloud.transform.position, cloud.transform.rotation);

                GameObject.Find("GodMode").GetComponent<GodMode>().SetActiveCloud();

                anim.SetTrigger("JumpUp");

                StartCountdown(3.125f + 2f);

                jumpUp = false;
            }
            else if (startCountdown)
            {
                gameController.GetComponent<GameController>().StartCountdown();
                startCountdown = false;
            }
            else if (attack)
            {
                anim.SetTrigger("Attack");
                attack = false;
            }
            else if (fireCollider)
            {
                transform.GetComponent<CapsuleCollider>().enabled = true;
                fireCollider = false;
            }
            else if (damage)
            {
                anim.SetTrigger("Impact");
                GetUp(1f + 2f);
                damage = false;
            }
            else if (getUp)
            {
                anim.SetTrigger("GetUp");
                getUp = false;
            }
            else if (die)
            {
                anim.SetTrigger("Die");
                die = false;
            }
        }
    }

    public void InitialJump(float d)
    {
        canvas.SetActive(true);
        a.Play("BowserInitialLaugh");
        initialJump = true;
        delay = d;
    }

    public void Roar(float d)
    {
        roar = true;
        delay = d; 
    }

    public void JumpUp(float d)
    {
        jumpUp = true;
        delay = d; 
    }

    public void StartCountdown(float d)
    {
        startCountdown = true;
        delay = d;
    }

    public void Attack(float d)
    {
        attack = true;
        delay = d;
    }

    public void EnablefireCollider(float d)
    {
        fireCollider = true;
        delay = d;
    }

    public void Impact(float d)
    {
        --life;

        //print("BOWSER LIFE: " + life + "/10");

        lifebar.value = life;

        if (life == 0)
        {
            die = true;
        }
        else
        {
            damage = true;
        }

        delay = d;
    }

    public void GetUp(float d)
    {
        getUp = true;
        delay = d;
    }

    public void FirebreathEvent()
    {
        fireBreath.GetComponent<ParticleSystem>().Play();
        EnablefireCollider(1f);
    }

    public void StopFirebreathEvent()
    {
        fireBreath.GetComponent<ParticleSystem>().Stop();
        transform.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void CameraShakeEvent(float newDuration)
    {
        CameraShake cs = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        cs.SetPower(0.05f);
        cs.SetDuration(newDuration);
        cs.Shake();
    }

    public void BowserRoarEvent()
    {
        a.Play("BowserRoar");
    }

    public void BowserBreath()
    {
        a.Play("BowserBreath");
    }

    public void BowserHop()
    {
        a.Play("BowserHop");
    }

    public void BowserLand()
    {
        a.Play("BowserLand");
    }

    public void BowserDamage()
    {
        if (life > 0)
        {
            a.Play("BowserDamage");
        }
        else
        {
            a.Play("BowserDead");
        }
    }

    public void BowserGetUp()
    {
        a.Play("BowserGetUp");
    }

    public void BowserFireFlame()
    {
        a.Play("BowserFireFlame");
    }

    public void SetLife(int l)
    {
        life = l;
    }
}
