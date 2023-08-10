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
using UnityEngine.Video;

public class GameController : MonoBehaviour
{

    public GameObject startMenuPrefab;
    private GameObject startMenu;

    private bool startButtonPressed;

    public GameObject pauseMenuPrefab;
    private GameObject pauseMenu;

    public GameObject pauseIconPrefab;
    private GameObject pauseIcon;

    private bool startCountdown = false;
    private bool started = false;
    private GameObject ball;
    private GameObject boss;

    public float frontSpeed = 7f;
	public float lateralStep = 0.1f;
    public Vector3 ballStartPosition = new Vector3(0f,0.5f,4f);

    private bool disableCollider = false;
    private bool disableMovement = false;

    private float countdown;

    public GameObject videoPlayerPrefab;
    private GameObject videoPlayer;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");
        startButtonPressed = false;

        // Crea l'start menu i afegeix el num. de monedes i el titol
        startMenu = (GameObject)Instantiate(startMenuPrefab, startMenuPrefab.transform.position, startMenuPrefab.transform.rotation);

        audioManager = FindObjectOfType<AudioManager>();
        startMenu.transform.GetChild(4).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "x   " + audioManager.getCoin(SceneManager.GetActiveScene().buildIndex) +" / 10";

        startMenu.transform.GetChild(5).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "" + getName(SceneManager.GetActiveScene().buildIndex);


        countdown = 5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

		if (startButtonPressed && !startCountdown)
		{
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                //print("... starting animation ...");
                boss.GetComponent<BowserManager>().InitialJump(3f);
            }
            else
            {
                StartCountdown();
            }

            startButtonPressed = false;
        }
		else if (Input.GetKeyDown(KeyCode.R))
		{
			started = false;
		}

        if (startCountdown)
        {
            if (countdown > 0f) countdown -= Time.fixedDeltaTime;

            else if (!started)
            {
                if (SceneManager.GetActiveScene().buildIndex != 3)
                {
                    Destroy(videoPlayer);
                    audioManager.Play("themeLevel" + SceneManager.GetActiveScene().buildIndex);
                }
                pauseIcon = (GameObject)Instantiate(pauseIconPrefab, pauseIconPrefab.transform.position, pauseIconPrefab.transform.rotation);
                started = true;
                ball.GetComponent<InitialJump>().enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ball.GetComponent<SphereCollider>().enabled = disableCollider;
            disableCollider = !disableCollider;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ball.GetComponent<FrontMovement>().enabled = disableMovement;

            GameObject Camera = GameObject.FindGameObjectWithTag("MainCamera");
            Camera.GetComponent<FrontMovement>().enabled = disableMovement;

            GameObject BackGroundImage = GameObject.FindGameObjectWithTag("BackgroundImage");
            BackGroundImage.GetComponent<FrontMovement>().enabled = disableMovement;

            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                GameObject.FindGameObjectWithTag("Boss").GetComponent<FrontMovement>().enabled = disableMovement;
                GameObject.FindGameObjectWithTag("Cloud").GetComponent<FrontMovement>().enabled = disableMovement;
            }

            disableMovement = !disableMovement;
        }

    }

    public bool GameStarted()
    {
        return started;
    }

    public void StartCountdown()
    {
        startCountdown = true;
        audioManager.Play("raceSound");
    }
    
    public void pressStartButton()
    {
        GameObject.FindGameObjectWithTag("GameController").SendMessage("setStartButtonTrue");

    }

    public void setStartButtonTrue()
    {
        audioManager.Pause("menuTheme");
        startButtonPressed = true;
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            videoPlayer = (GameObject)Instantiate(videoPlayerPrefab, videoPlayerPrefab.transform.position, videoPlayerPrefab.transform.rotation);
            videoPlayer.GetComponent<VideoPlayer>().targetCamera = Camera.main;
        }
        else audioManager.Play("themeLevel" + SceneManager.GetActiveScene().buildIndex);

        Destroy(startMenu);
    }

    public void pressMouseButton()
    {
        GameObject.FindGameObjectWithTag("GameController").SendMessage("setMouseButton");

    }

    public void setMouseButton()
    {
        ball.GetComponent<BallSideMovement>().SendMessage("setMouseMovement",true);
        startMenu.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
        startMenu.transform.GetChild(3).GetChild(2).gameObject.SetActive(true);
    }


    public void pressKeyboardButton()
    {
        GameObject.FindGameObjectWithTag("GameController").SendMessage("setKeyboardButton");
    }

    public void setKeyboardButton()
    {
        ball.GetComponent<BallSideMovement>().SendMessage("setMouseMovement", false);
        startMenu.transform.GetChild(3).GetChild(2).gameObject.SetActive(false);
        startMenu.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
    }

    public string getName(int level)
    {
        if(level == 1)
        {
            return "MARIO  CIRCUIT";
        }
        else if(level == 2)
        {
            return "SKY  GARDEN";
        }
        else if(level == 3)
        {
            return "BOWSER'S  MANSION";
        }
        return "";
    }

    public void pressPauseGame()
    {
        GameObject.FindGameObjectWithTag("GameController").SendMessage("pauseGame");

    }


    public void pauseGame()
    {
        pauseMenu = (GameObject)Instantiate(pauseMenuPrefab, pauseMenuPrefab.transform.position, pauseMenuPrefab.transform.rotation);

        pauseMenu.transform.GetChild(2).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "x   " + audioManager.getActualCoins() + " / 10";
        pauseMenu.transform.GetChild(3).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "" + getName(SceneManager.GetActiveScene().buildIndex);

        audioManager.Pause("themeLevel" + SceneManager.GetActiveScene().buildIndex);
        audioManager.unPause("menuTheme");

        Destroy(pauseIcon);
        Time.timeScale = 0f;
    }

    public void pressContinueButton()
    {
        GameObject.FindGameObjectWithTag("GameController").SendMessage("continueGame");
    }

    public void continueGame()
    {
        pauseIcon = (GameObject)Instantiate(pauseIconPrefab, pauseIconPrefab.transform.position, pauseIconPrefab.transform.rotation);
        Destroy(pauseMenu);

        audioManager.Pause("menuTheme");
        audioManager.unPause("themeLevel" + SceneManager.GetActiveScene().buildIndex);

        ball.GetComponent<BallSideMovement>().RestartMousePosition();
        Time.timeScale = 1f;
    }

}
