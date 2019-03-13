/*////////////////////////////////////////////////////////////////////////////////////////////////*/
/// <summary>
/// SensorObject.cs
/// </summary>
/*///////////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SensorObjectType
{
    AUTOMATIC,
    MANUAL
};

public class SensorObject : MonoBehaviour
{
    #region PROPERTIES
    [Header("SENSOR")]
    [SerializeField] Sensor sensor;
    public Sensor _Sensor
    {
        get { return sensor; }
        set { sensor = value; }
    }
    [SerializeField] SensorObjectType sensorObjectType = SensorObjectType.AUTOMATIC;
    public SensorObjectType _SensorObjectType
    {
        get { return sensorObjectType; }
        set { sensorObjectType = value; }
    }
    #endregion
    
    #region METHODS
    public virtual void Activate(SensorData _sensorData) { }
    public virtual void ActivateManually(GameObject _activator) { }
    #endregion
}
