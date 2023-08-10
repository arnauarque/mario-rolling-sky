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
using System.IO;

public class FloorManagerLevel3 : MonoBehaviour
{
    public TextAsset tilemap3;

    private GameController gameController;
    public GameObject transparentTile;
    public GameObject basicTile;
    public GameObject fallingBasicTile;
    public GameObject jumpTile;
    public GameObject boostTile;
    public GameObject fallingTile;
    public GameObject forwardTile;
    public GameObject forwardTile2;
    public GameObject forwardTile3;
    public GameObject forwardTile4;
    public GameObject forwardTile5;
    public GameObject autoMoveTile;

    public GameObject endPlane;
    public GameObject endFlag;

    private float tilemapAlfa = 1f;

    private int offsetJ = 1;

    // Start is called before the first frame update
    void Start()
    {

        string txt = tilemap3.text;
        StringReader reader = new StringReader(txt);
        tilemapAlfa = 1f;

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
            for (int j = 0; j < cols; j += offsetJ)
            {
                offsetJ = 1;

                if (line[j] != '+')
                {
                    CreateTile(line[j], new Vector3(tilemapAlfa * (float)((j - 2)), 0f, (float)i));
                }
            }
        }

        SetUpEndOfLevel(rows, cols);
    }

    private int ToInteger(string number)
    {
        int centenes = number[0] - '0';
        int desenes = number[1] - '0';
        int unitats = number[2] - '0';

        return centenes * 100 + desenes * 10 + unitats;
    }

    private void CreateTile(char tile, Vector3 position)
    {
        GameObject newTile;
        if (tile == '0') newTile = basicTile;
        else if (tile == 'L')
        {
            newTile = fallingBasicTile;
            position.y = 4f;
        }
        else if (tile == 'J') newTile = jumpTile;
        else if (tile == 'D') newTile = fallingTile;
        else if (tile == 'F') newTile = forwardTile;
        else if (tile == 'B') newTile = boostTile;
        else if (tile == 'A')
        {
            GameObject aux = (GameObject)Instantiate(transparentTile, position, transform.rotation);

            newTile = autoMoveTile;
        }
        else if (tile == '2')
        {
            newTile = forwardTile2;
            offsetJ = 2;
        }
        else if (tile == '3')
        {
            newTile = forwardTile3;
            offsetJ = 3;
        }
        else if (tile == '4')
        {
            newTile = forwardTile4;
            offsetJ = 4;
        }
        else if (tile == '5')
        {
            newTile = forwardTile5;
            offsetJ = 5;
        }
        else newTile = transparentTile;

        GameObject obj = (GameObject)Instantiate(newTile, position, newTile.transform.rotation);
    }

    private void SetUpEndOfLevel(int rows, int cols)
    {

        Instantiate(endPlane, new Vector3(0f, 0f, rows + 1f), endPlane.transform.rotation);

        for (int i = -3; i < 4; ++i)
        {
            for (int j = rows; j < rows + 7; ++j)
            {
                Instantiate(basicTile, new Vector3(i, 0f, j), basicTile.transform.rotation);
            }
        }

        Instantiate(endFlag, new Vector3(0f, 0.28f, rows + 3), endFlag.transform.rotation);
    }
}
