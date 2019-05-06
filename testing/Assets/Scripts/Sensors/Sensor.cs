/*////////////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — Sensor
/// PURPOSE — detect targets from list and alert sensor objects so they can perform actions
/*///////////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    #region PROPERTIES
    [Header("SENSOR")]
    [SerializeField] GameObject owner;
    public GameObject _Owner
    {
        get { return owner; }
        private set { owner = value; }
    }
    [SerializeField] InteractiveStatus interactiveStatus = InteractiveStatus.ENABLED;
    public InteractiveStatus _InteractiveStatus
    {
        get { return interactiveStatus; }
        set { interactiveStatus = value; }
    }

    [Header("TARGETS TO TRACK")]
    [TagSelector]
    [SerializeField] string[] targets;
    
    [Header("SENSOR OBJECTS TO ALERT")]
    [SerializeField] List<SensorObject> sensorObjects;
    public List<SensorObject> _SensorObjects
    {
        get { return sensorObjects; }
        private set { sensorObjects = value; }
    }
    #endregion
    
    #region TRIGGERS
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="_other">The other Collider involved in this collision.</param>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter(Collider _other)
    {
        if(_InteractiveStatus == InteractiveStatus.ENABLED)
        {
            for(int _index = 0; _index < targets.Length; ++_index)
            {
                if(_other.gameObject.tag == targets[_index])
                {
                    //capture data
                    SensorData _sensorData = new SensorData(this, SensorStatus.ENTERED, _other.gameObject);
                    Activate(_sensorData);
                }
            }
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="_other">The other Collider involved in this collision.</param>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void OnTriggerStay(Collider _other)
    {
        if(_InteractiveStatus == InteractiveStatus.ENABLED)
        {
            for(int _index = 0; _index < targets.Length; ++_index)
            {
                if(_other.gameObject.tag == targets[_index])
                {
                    //capture data
                    SensorData _sensorData = new SensorData(this, SensorStatus.PERSISTS, _other.gameObject);
                    Activate(_sensorData);
                }
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
        if(_InteractiveStatus == InteractiveStatus.ENABLED)
        {
            for(int _index = 0; _index < targets.Length; ++_index)
            {
                if(_other.gameObject.tag == targets[_index])
                {
                    //capture data
                    SensorData _sensorData = new SensorData(this, SensorStatus.EXITED, _other.gameObject);
                    Activate(_sensorData);
                }
            }
        }
    }
    #endregion
    #region METHODS
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Activate
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Activate(SensorData _sensorData)
    {
        for(int _i = 0; _i < sensorObjects.Count; ++_i)
        {
            sensorObjects[_i].Activate(_sensorData);
            Events.instance.Raise(new EVENT_SENSOR_BROADCAST(_sensorData));
        }
    }    
    #endregion
}
