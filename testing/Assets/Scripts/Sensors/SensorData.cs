/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — SensorData
/// PURPOSE — used to pass information from sensor to sensor object
/*///////////////////////////////////////////////////////////////////////////////////////////*/

using UnityEngine;

public enum SensorStatus
{
    ENTERED,
    PERSISTS,
    EXITED
};

public struct SensorData
{
    public Sensor sensor;
    public SensorStatus status;
    public GameObject detectee;

    public SensorData(Sensor _sensor, SensorStatus _status, GameObject _detectee)
    {
        sensor = _sensor;
        status = _status;
        detectee = _detectee;
    }
}
