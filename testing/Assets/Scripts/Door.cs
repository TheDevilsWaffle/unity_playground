using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorStatus
{
    OPEN,
    CLOSE,
    DISABLED
};

public class Door : SensorObject
{
    [SerializeField] Sensor sensor;
    [SerializeField] DoorStatus status;
    [SerializeField] Vector3 offset;
    [SerializeField] AnimationCurve openCurve;
    [SerializeField] float openTime;
    [SerializeField] AnimationCurve closeCurve;
    [SerializeField] float closeTime;

    Transform tr;

    void OnValidate()
    {
        tr = GetComponent<Transform>();

        //UpdateDoorStatus(status);
    }
    void Awake()
    {
        OnValidate();
    }
    public override void Activate(SensorData _sensorData)
    {
        if(_sensorData.status == SensorStatus.ENTERED)
        {
            UpdateDoorStatus(DoorStatus.OPEN);
        }
        else if(_sensorData.status == SensorStatus.EXITED)
        {
            UpdateDoorStatus(DoorStatus.CLOSE);
        }
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// determine what to do based on door's status
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void UpdateDoorStatus(DoorStatus _status)
    {
        status = _status;

        if(_status == DoorStatus.CLOSE)
        {
            CloseDoor();
        }
        else if (_status == DoorStatus.OPEN)
        {
            OpenDoor();
        }
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// description
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void CloseDoor()
    {
        if(LeanTween.isTweening(this.gameObject))
        {
            LeanTween.cancel(this.gameObject);
        }

        LeanTween.moveLocal(this.gameObject, Vector3.zero, closeTime).setEase(closeCurve);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// description
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void OpenDoor()
    {
        if(LeanTween.isTweening(this.gameObject))
        {
            LeanTween.cancel(this.gameObject);
        }
        
        LeanTween.moveLocal(this.gameObject, offset, openTime).setEase(openCurve);
    }
}
