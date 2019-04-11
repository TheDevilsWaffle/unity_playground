/*////////////////////////////////////////////////////////////////////////////////////////////////*/
/// <summary>
/// Sensor.cs
/// </summary>
/*///////////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sensor : MonoBehaviour
{
    #region PROPERTIES
    [Header("OWNER")]
    [SerializeField] SensorObject owner;
    public SensorObject _Owner
    {
        get { return owner; }
        private set { owner = value; }
    }
    [Header("TARGETS TO TRACK")]
    [TagSelector]
    [SerializeField] string[] targets;
    #endregion
    bool IsTarget(string _tag)
    {
        for (int _index = 0; _index < targets.Length; ++_index)
        {
            if (_tag == targets[_index])
                return true;
        }
        return false;
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
        if (IsTarget(_other.gameObject.tag))
            owner.SensorAlert(new SensorData(this.gameObject, _other.gameObject, TriggerStatus.ENTER));
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
        if (IsTarget(_other.gameObject.tag))
            owner.SensorAlert(new SensorData(this.gameObject, _other.gameObject, TriggerStatus.STAY));
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// OnTriggerEnter is called when the Collider other exits the trigger.
    /// </summary>
    /// <param name="_other">The other Collider involved in this collision.</param>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void OnTriggerExit(Collider _other)
    {
        if (IsTarget(_other.gameObject.tag))
            owner.SensorAlert(new SensorData(this.gameObject, _other.gameObject, TriggerStatus.EXIT));
    }
    #endregion
}