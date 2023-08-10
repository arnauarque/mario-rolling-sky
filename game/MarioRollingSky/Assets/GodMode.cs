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

public class GodMode : MonoBehaviour
{

    public int tilemapSize;

    private GameObject ball;
    private GameObject mainCamera;
    private GameObject background;
    private GameObject boss;
    private GameObject cloud;

    private float offsetCamera;
    private float offsetBackground;
    private float offsetBoss;
    private float offsetCloud;

    private bool level3;
    private bool cloudInfoSet;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3) level3 = true;
        else level3 = false;

        cloudInfoSet = false;

        ball = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        background = GameObject.FindGameObjectWithTag("BackgroundImage");

        if (level3)
        {
            boss = GameObject.FindGameObjectWithTag("Boss");
            offsetBoss = boss.transform.position.z - ball.transform.position.z;
            cloud = null;
        }

        offsetCamera = mainCamera.transform.position.z - ball.transform.position.z;
        offsetBackground = background.transform.position.z - ball.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            UpdateZ(0f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateZ(tilemapSize / 5f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateZ((2f * tilemapSize) / 5f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateZ((3f * tilemapSize) / 5f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpdateZ((4f * tilemapSize) / 5f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UpdateZ((5f * tilemapSize) / 5f);
        }
    }

    private void UpdateZ(float z)
    {
        ball.transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y, z);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, z + offsetCamera);
        background.transform.position = new Vector3(background.transform.position.x, background.transform.position.y, z + offsetBackground);

        if (level3)
        {
            boss.transform.position = new Vector3(boss.transform.position.x, boss.transform.position.y, z + offsetBoss);
            if (cloudInfoSet) cloud.transform.position = new Vector3(cloud.transform.position.x, cloud.transform.position.y, z + offsetCloud);
        }
    }

    public void SetActiveCloud()
    {
        cloud = GameObject.FindGameObjectWithTag("Cloud");
        offsetCloud = cloud.transform.position.z - ball.transform.position.z;
        cloudInfoSet = true;
    }
}
