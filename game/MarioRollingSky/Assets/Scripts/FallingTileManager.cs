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

public class FallingTileManager : MonoBehaviour
{

    private bool falling;

    // Start is called before the first frame update
    void Start()
    {
        falling = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Color c = gameObject.GetComponent<Renderer>().material.color;

        if (gameObject.scene.name == "Level1") gameObject.GetComponent<Renderer>().material.color = new Color(c.r, c.g - 0.05f, c.b);
        else if (gameObject.scene.name == "Level2") gameObject.GetComponent<Renderer>().material.color = new Color(c.r - 0.05f, c.g - 0.05f, c.b - 0.05f);
        else gameObject.GetComponent<Renderer>().material.color = new Color(c.r, c.g - 0.05f, c.b);

        if (falling)
        {
            transform.Translate(Vector3.down * 0.1f, Space.World);

            if (transform.position.y < -20f)
            {
                falling = false;
                Destroy(gameObject);
            }
        }
    }
    void setFalling(bool f)
    {
        falling = f;
    }
}
