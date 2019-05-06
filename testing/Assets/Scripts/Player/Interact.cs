/*////////////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — Interact.cs
/// PURPOSE — generic ability to interact w/ something that is interactable
/*///////////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : SensorObject
{
    #region PROPERTIES
    [Header("INTERACT")]
    [SerializeField] KeyCode interactKey = KeyCode.E;
    List<GameObject> interactables = new List<GameObject>();
    GameObject currentInteractable = null;
    #endregion

    #region INITIALIZATION
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        currentInteractable = null;
        interactables = new List<GameObject>();
    }
    #endregion

    #region UPDATE
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if(Input.GetKeyDown(interactKey))
            AttemptInteract();
    }    
    #endregion
    
    #region METHODS
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// Activate
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public override void Activate(SensorData _sensorData)
    {
        if(_sensorData.detectee.tag == "Interactable")
        {
            if(_sensorData.status == SensorStatus.ENTERED || _sensorData.status == SensorStatus.PERSISTS && !IsItemInList(_sensorData.detectee))
                interactables.Add(_sensorData.detectee);

            else if(_sensorData.status == SensorStatus.EXITED && IsItemInList(_sensorData.detectee))
                interactables.Remove(_sensorData.detectee);
        }
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// attempt to interact w/ the thing
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void AttemptInteract()
    {
        if(interactables.Count >= 1)
        {
            currentInteractable = interactables[0];
            if(currentInteractable.GetComponent<SensorObject>()._SensorObjectType == SensorObjectType.MANUAL)
                currentInteractable.GetComponent<SensorObject>().ActivateManually(_Sensor._Owner);

            interactables.Remove(interactables[0]);
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// return whether or not this interactable is in the list of things we can interact with
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    bool IsItemInList(GameObject _go)
    {
        if(interactables.Contains(_go))
            return true;
        else
            return false;
    }
    #endregion
}
