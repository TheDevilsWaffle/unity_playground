/*////////////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — DoorControls
/// PURPOSE — allow for manual open/close of door by characters
/*///////////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractiveStatus
{
    ENABLED,
    DISABLED
};

public class DoorControls : SensorObject
{
    #region PROPERTIES
    [SerializeField] InteractiveStatus interactiveStatus = InteractiveStatus.ENABLED;
    public InteractiveStatus _InteractiveStatus
    {
        get { return interactiveStatus; }
        set { interactiveStatus = value; }
    }
    [Header("CONTROLS")]
    [SerializeField] Door door;
    
    #endregion
    #region METHODS
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// ActivateManually
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    public override void ActivateManually(GameObject _activator)
    {
        if(_InteractiveStatus == InteractiveStatus.ENABLED)
        {
            if(door._DoorStatus == DoorStatus.CLOSED)
                door._DoorStatus = DoorStatus.OPEN;
            else if(door._DoorStatus == DoorStatus.OPEN)
                door._DoorStatus = DoorStatus.CLOSED;
        }
    }   
    #endregion
}
