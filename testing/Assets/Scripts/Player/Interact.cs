/*////////////////////////////////////////////////////////////////////////////////////////////////*/
/// <summary>
/// Interact.cs
/// </summary>
/*///////////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : SensorObject
{
    #region PROPERTIES
    [Header("INTERACT")]
    [SerializeField] KeyCode interactKey = KeyCode.E;
    [SerializeField] Sensor sensor;
    #endregion
    #region UPDATE
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if (Input.GetKeyDown(interactKey) && _IsReadyToActivate)
            AttemptInteract();
    }
    #endregion
    #region METHODS
    void AttemptInteract()
    {
        
    }
    #endregion
}
