using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : SensorObject
{
    [SerializeField] KeyCode interactKey = KeyCode.E;
    [SerializeField] Sensor sensor;
    List<GameObject> interactables = new List<SensorObject>();


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        interactables = new List<GameObject>();
    }
    #region UPDATE
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if(Input.GetKeyDown(interactKey))
        {
            AttemptInteract();
        }
    }    
    #endregion

    void AttemptInteract()
    {
        for
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// Activate
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
}
