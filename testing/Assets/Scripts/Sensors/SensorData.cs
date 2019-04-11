using UnityEngine;

public enum TriggerStatus
{
    ENTER,
    STAY,
    EXIT,
    DISABLED
};

public struct SensorData
{
    public GameObject _DetectorObject;
    public GameObject _DetectedObject;
    public TriggerStatus _TriggerStatus;

    public SensorData(GameObject _detectorObject, GameObject _detectedObject, TriggerStatus _triggerStatus)
    {
        _DetectorObject = _detectorObject;
        _DetectedObject = _detectedObject;
        _TriggerStatus = _triggerStatus;
    }
}
