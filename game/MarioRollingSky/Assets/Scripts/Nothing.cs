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

public class Nothing : MonoBehaviour
{

    public bool printInfoAlways;
    public bool printPositions;
    public bool printDifference;

    private float[] positions;
    private int pointer;

    // Start is called before the first frame update
    void Start()
    {
        printInfoAlways = false;
        printPositions = false;
        positions = new float[10];

        for (int i = 0; i < positions.Length; ++i)
        {
            positions[i] = -1;
        }

        pointer = 0;

        print(Screen.width + " x " + Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (printInfoAlways) print(Input.mousePosition);

        if (printPositions)
        {
            for (int i = 0; i < positions.Length; ++i)
            {
                if (positions[i] > 0f) print("Position " + i + ": " + positions[i]);
            }
            printPositions = false;
        }

        if (printDifference)
        {
            for (int i = 0; i < positions.Length; i += 2)
            {
                if (positions[i] > 0) print("Difference: " + (positions[i + 1] - positions[i]));
            }
            printDifference = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            print("INFO: Posicio guardada.");
            positions[pointer] = Input.mousePosition.x;
            ++pointer;
        }
    }
}
