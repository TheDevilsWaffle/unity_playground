/*////////////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — Door
/// PURPOSE — door that is activatable via door controls or automatic sensor
/*///////////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorStatus
{
    OPEN,
    CLOSED,
    DISABLED
};

public class Door : SensorObject
{
    #region PROPERTIES
    [Header("DOOR")]
    [SerializeField] DoorStatus doorStatus = DoorStatus.CLOSED;
    public DoorStatus _DoorStatus
    {
        get { return doorStatus; }
        set { 
                doorStatus = value;
                UpdateDoor(doorStatus);
            }
    }
    [SerializeField] Vector3 offsetPosition = new Vector3(0, 0, -1f);
    Transform tr;
    
    [Header("ANIMATION")]
    [SerializeField] AnimationCurve openCurve;
    [SerializeField] float openTime;
    [SerializeField] AnimationCurve closeCurve;
    [SerializeField] float closeTime;
    #endregion

    #region INITIALIZATION
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Awake
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        tr = GetComponent<Transform>();
    }
    #endregion
    
    #region METHODS
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Activate
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    public override void Activate(SensorData _sensorData)
    {
        if(_SensorObjectType == SensorObjectType.AUTOMATIC)
        {
            //door is closed, open it
            if(_sensorData.status == SensorStatus.ENTERED)
                _DoorStatus = DoorStatus.OPEN;
            //door is open, close it
            else if(_sensorData.status == SensorStatus.EXITED)
                _DoorStatus = DoorStatus.CLOSED;
        }
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// determine what to do based on door's status
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    public void UpdateDoor(DoorStatus _status)
    {
        if(_status == DoorStatus.CLOSED)
            CloseDoor();
        else if (_status == DoorStatus.OPEN)
            OpenDoor();
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// animate close
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void CloseDoor()
    {
        if(LeanTween.isTweening(this.gameObject))
            LeanTween.cancel(this.gameObject);

        LeanTween.moveLocal(this.gameObject, Vector3.zero, closeTime).setEase(closeCurve);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// animate open
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void OpenDoor()
    {
        if(LeanTween.isTweening(this.gameObject))
            LeanTween.cancel(this.gameObject);
        
        LeanTween.moveLocal(this.gameObject, offsetPosition, openTime).setEase(openCurve);
    }
    #endregion
}