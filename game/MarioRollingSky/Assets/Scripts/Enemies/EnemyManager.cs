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
using UnityEngine.SceneManagement;
using System.IO;

public class EnemyManager : MonoBehaviour
{
    public TextAsset tilemap1enemies;
    public TextAsset tilemap2enemies;
    public TextAsset tilemap3enemies;

    private Scene scene;

    public GameObject marioPipe;
    public GameObject marioChompL;
    public GameObject marioChompR;
    public GameObject marioHammerLeft;
    public GameObject marioHammerRight;
    public GameObject marioThwomp;
    public GameObject marioPiranha;
    public GameObject marioMysteryBlock; 
    public GameObject marioBrickBlock;
    public GameObject marioBillBlaster;
    public GameObject marioCoin;
    public GameObject marioRedKoopa;
    public GameObject marioBlueShell;
    public GameObject marioFlower;
    public GameObject attackPlane;

    private float tilemapAlfa = 1f;

    private float pipeControl=0f;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();

        StringReader reader;

        if (scene.buildIndex == 1)
        {
            string txt = tilemap1enemies.text;
            reader = new StringReader(txt);
            tilemapAlfa = -1f;
        }

        else if(scene.buildIndex == 2)
        {
            string txt = tilemap2enemies.text;
            reader = new StringReader(txt);
            tilemapAlfa = 1f;
        }

        else if (scene.buildIndex == 3)
        {
            string txt = tilemap3enemies.text;
            reader = new StringReader(txt);
            tilemapAlfa = 1f;
        }

        else
        {
            print("Error: Wrong scene index.");
            reader = null;
        }

        //Llegim el titol del Tilemap
        reader.ReadLine();

        /* !! WARNING !!
         * Rows i Cols han d'estar a dues linies separades del .txt.
         * En format CDU (Centenes, Desenes i Unitats).
         * [Exemple: 012]
         */

        //Llegim el tamany del Tilemap
        int rows = ToInteger(reader.ReadLine());
        int cols = ToInteger(reader.ReadLine());

        string line;

        for (int i = 0; i < rows; ++i)
        {
            line = reader.ReadLine();
            for (int j = 0; j < cols; ++j)
            {
                if (line[j] != ' ')
                {
                    CreateEnemy(line[j], new Vector3((float)tilemapAlfa * (j - 2), 0f, (float)i));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private int ToInteger(string number)
    {
        int centenes = number[0] - '0';
        int desenes = number[1] - '0';
        int unitats = number[2] - '0';

        return centenes * 100 + desenes * 10 + unitats;
    }
    //brick,question,hammer,pipe,piranha,chomp
    private void CreateEnemy(char enemy, Vector3 position)
    {
        GameObject newEnemy;
        if (enemy == 'P')
        {
            newEnemy = marioPipe;
            GameObject obj = (GameObject)Instantiate(newEnemy, position, newEnemy.transform.rotation);
            if(pipeControl!=position.z) obj.SendMessage("SetHasSound", true);
            else obj.SendMessage("SetHasSound", false);
            pipeControl = position.z;

            obj.transform.parent = transform;
        }
        else if (enemy == 'C')
        {
            newEnemy = marioChompL;
            Vector3 p = new Vector3(position.x +0.83476247684f, position.y + 0.5376891f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
        }
        else if (enemy == 'c')
        {
            newEnemy = marioChompR;

            Quaternion r = newEnemy.transform.rotation;
            Quaternion q = Quaternion.Euler(0f, 0f, 180f);
            Vector3 p = new Vector3(position.x - 0.83476247684f, position.y + 0.5376891f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, r * q );
            obj.transform.parent = transform;
        }
        else if (enemy == 'H')
        {
            newEnemy = marioHammerLeft;
            Vector3 p = new Vector3(position.x - 0.3f , position.y + 0.1f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
        }
        else if (enemy == 'h')
        {
            newEnemy = marioHammerRight;
            Vector3 p = new Vector3(position.x + 0.3f, position.y + 0.1f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
        }
        else if (enemy == '-')
        {

        }
        else if (enemy == 'T')
        {
            newEnemy = marioThwomp;
            Vector3 p = new Vector3(position.x + 0.5f, 2f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
        }
        else if (enemy == 'N')
        {
            newEnemy = marioPiranha;
            Vector3 p = new Vector3(position.x, position.y + 0.1f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
        }
        else if (enemy == 'n')
        {
            newEnemy = marioPiranha;
            //Quaternion r = newEnemy.transform.rotation;
            //Quaternion q = Quaternion.Euler(0f, 180f, 0f);
            Vector3 p = new Vector3(position.x, position.y + 0.1f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
            obj.transform.Rotate(0f, 180f, 0f, Space.World);
        }
        else if (enemy == 'p')
        {
            newEnemy = marioPiranha;
            Vector3 p = new Vector3(position.x, position.y + 0.1f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
            obj.transform.Rotate(0f, 90f, 0f, Space.World);
        }
        else if (enemy == 'M')
        {
            newEnemy = marioMysteryBlock;
            Vector3 p = new Vector3(position.x, position.y+0.5f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
        }
        else if (enemy == 'B')
        {
            newEnemy = marioBrickBlock;
            Vector3 p = new Vector3(position.x, position.y+0.6f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
        }
        else if (enemy == ')')
        {
            newEnemy = marioBillBlaster;
            Vector3 p = new Vector3(position.x, position.y + 0.1f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
        }
        else if (enemy == '(')
        {
            newEnemy = marioBillBlaster;
            Vector3 p = new Vector3(position.x, position.y + 0.1f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
            obj.transform.Rotate(0f, 180f, 0f, Space.World);
        }
        else if (enemy == '|')
        {
            newEnemy = marioBillBlaster;
            Vector3 p = new Vector3(position.x, position.y + 0.1f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
            obj.transform.Rotate(0f, 90f, 0f, Space.World);
        }
        else if (enemy == 'G')
        {
            newEnemy = marioCoin;
            Vector3 p = new Vector3(position.x, position.y + 0.1f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
        }
        else if (enemy == 'K')
        {
            newEnemy = marioRedKoopa;
            Vector3 p = new Vector3(position.x, position.y + 0.6f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
            obj.transform.Rotate(0f, -90f, 0f, Space.World);
        }
        else if (enemy == 'k')
        {
            newEnemy = marioRedKoopa;
            Vector3 p = new Vector3(position.x, position.y + 0.6f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
            obj.transform.Rotate(0f, 90f, 0f, Space.World);
        }
        else if (enemy == 'l')
        {
            newEnemy = marioRedKoopa;
            Vector3 p = new Vector3(position.x, position.y + 0.6f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
        }
        else if (enemy == 'S')
        {
            newEnemy = marioBlueShell;
            Vector3 p = new Vector3(position.x, position.y + 1.1f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
        }
        else if (enemy == 'F')
        {
            newEnemy = marioFlower;
            Vector3 p = new Vector3(position.x, position.y + 0.1f, position.z);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
        }
        else if (enemy == 'A')
        {
            newEnemy = attackPlane;
            Vector3 p = new Vector3(position.x, position.y, position.z - 38f);
            GameObject obj = (GameObject)Instantiate(newEnemy, p, newEnemy.transform.rotation);
            obj.transform.parent = transform;
        }
        else
        {
            Debug.Log("Wrong enemy tilemap char: " + enemy);
        }
  
    }
}
