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

public class JumpingTileManager : MonoBehaviour
{

    public float maxHeightJumpTile = 1f;

    private bool jumping;
    private float y;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("jumpSound");
        jumping = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (jumping)
        {
            if ((y - distance) <= maxHeightJumpTile)
            {
                transform.position = new Vector3(transform.position.x, y - distance, transform.position.z);
            }

            else jumping = false;
        }
        
        else if (transform.position.y > 0f)
        {
            transform.Translate(Vector3.down * 0.1f, Space.World);
        }
    }

    public void SetY(float newY)
    {
        y = newY;
    }

    public void SetDistanceToBall(float dist)
    {
        distance = dist;
    }
}
