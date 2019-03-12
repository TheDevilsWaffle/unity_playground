using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public enum SensorType
{
    AUTOMATIC,
    MANUAL
};

public class Sensor : MonoBehaviour
{
    [SerializeField] InteractiveStatus status = InteractiveStatus.ENABLED;
    [SerializeField] string[] targets;
    [SerializeField] List<SensorObject> sensorObjects;
    #region TRIGGERS
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="_other">The other Collider involved in this collision.</param>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter(Collider _other)
    {
        if(status == InteractiveStatus.ENABLED)
        {
            for(int _index = 0; _index < targets.Length; ++_index)
            {
                if(_other.gameObject.tag == targets[_index])
                {
                    //capture data
                    SensorData _sensorData = new SensorData(this, SensorStatus.ENTERED, _other.gameObject);

                    //automatic
                    if(sensorType == SensorType.AUTOMATIC)
                    {
                        Activate(_sensorData);
                    }
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
        if(status == InteractiveStatus.ENABLED)
        {
            for(int _index = 0; _index < targets.Length; ++_index)
            {
                if(_other.gameObject.tag == targets[_index])
                {
                    //capture data
                    SensorData _sensorData = new SensorData(this, SensorStatus.PERSISTS, _other.gameObject);

                    //automatic
                    if(sensorType == SensorType.AUTOMATIC)
                    {
                        Activate(_sensorData);
                    }
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
        for(int _index = 0; _index < targets.Length; ++_index)
        {
            if(_other.gameObject.tag == targets[_index])
            {
                //capture data
                SensorData _sensorData = new SensorData(this, SensorStatus.EXITED, _other.gameObject);

                //automatic
                if(sensorType == SensorType.AUTOMATIC)
                {
                    Activate(_sensorData);
                }
            }
        }
    }

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
