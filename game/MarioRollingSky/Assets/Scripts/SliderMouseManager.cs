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

public class SliderMouseManager : MonoBehaviour
{

    private Image mouse;
    private float x;

    // Start is called before the first frame update
    void Start()
    {
        mouse = GetComponent<Image>();
        x = (((Mathf.Sin(Time.time) + 1f) / 2f) * 467f) + 83f;
        mouse.rectTransform.localPosition = new Vector3(x, -28f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        x = (((Mathf.Sin(Time.time) + 1f) / 2f) * 467f) + 83f;
        mouse.rectTransform.localPosition = new Vector3(x, -28f, 0f);
    }
}
