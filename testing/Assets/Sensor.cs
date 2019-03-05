using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class Sensor : MonoBehaviour
{
    [SerializeField] string[] targets;
    [SerializeField] List<SensorObject> sensorObjects;

    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called when the script instance is being loaded.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {

    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called before the first frame update
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        SetSubscriptions();
    }
    #region SUBSCRIPTIONS
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// RemoveSubscriptions()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void RemoveSubscriptions()
    {
        
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
        for(int _index = 0; _index < targets.Length; ++_index)
        {
            if(_other.gameObject.tag == targets[_index])
            {
                SensorData _sensorData = new SensorData(this, SensorStatus.ENTERED, _other.gameObject);

                Events.instance.Raise(new EVENT_SENSOR_BROADCAST(_sensorData));

                for(int _i = 0; _i < sensorObjects.Count; ++_i)
                {
                    sensorObjects[_i].Activate(_sensorData);
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
        for(int _index = 0; _index < targets.Length; ++_index)
        {
            if(_other.gameObject.tag == targets[_index])
            {
                SensorData _sensorData = new SensorData(this, SensorStatus.PERSISTS, _other.gameObject);

                Events.instance.Raise(new EVENT_SENSOR_BROADCAST(_sensorData));

                for(int _i = 0; _i < sensorObjects.Count; ++_i)
                {
                    sensorObjects[_i].Activate(_sensorData);
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
                SensorData _sensorData = new SensorData(this, SensorStatus.EXITED, _other.gameObject);

                Events.instance.Raise(new EVENT_SENSOR_BROADCAST(_sensorData));

                for(int _i = 0; _i < sensorObjects.Count; ++_i)
                {
                    sensorObjects[_i].Activate(_sensorData);
                }
            }
        }
    }
    #endregion
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called when the MonoBehaviour will be destroyed.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void OnDestroy()
    {
        RemoveSubscriptions();
    }
}
