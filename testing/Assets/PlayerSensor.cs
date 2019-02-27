using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public enum DoorStatus
{
    OPEN,
    CLOSE
};
public class PlayerSensor : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] DoorStatus status;
    [SerializeField] Vector3 offset;
    [SerializeField] AnimationCurve openCurve;
    [SerializeField] float openTime;
    [SerializeField] AnimationCurve closeCurve;
    [SerializeField] float closeTime;
    [SerializeField] string[] targets;

    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called when the script instance is being loaded.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        UpdateDoorStatus(status);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called before the first frame update
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        
    }
    #region TRIGGERS
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="_other">The other Collider involved in this collision.</param>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter(Collider _other)
    {
        for(int _index = 0; _index < targets.Length; ++_index)
        {
            if(_other.gameObject.tag == targets[_index])
            {
                print(_other.gameObject.name + " detected! open this door!");
                OpenDoor();
            }
        }
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// OnTriggerEnter is called when the Collider other exits the trigger.
    /// </summary>
    /// <param name="_other">The other Collider involved in this collision.</param>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void OnTriggerExit(Collider _other)
    {
        for(int _index = 0; _index < targets.Length; ++_index)
        {
            if(_other.gameObject.tag == targets[_index])
            {
                print(_other.gameObject.name + " detected! close this door!");
                CloseDoor();
            }
        }
    }
    #endregion
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
        //door.transform.localPosition = Vector3.zero;
        LeanTween.moveLocal(door, Vector3.zero, closeTime).setEase(closeCurve);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// description
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void OpenDoor()
    {
        //door.transform.localPosition += offset;
        LeanTween.moveLocal(door, offset, openTime).setEase(openCurve);
    }
}
