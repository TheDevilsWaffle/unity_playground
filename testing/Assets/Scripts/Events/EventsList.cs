using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region KEYBOARD

public class EVENT_KEYBOARD_KEY_BROADCAST : GameEvent
{
    public InputData inputData;
    public EVENT_KEYBOARD_KEY_BROADCAST(InputData _inputData)
    {
        inputData = _inputData;
    }
}

#endregion

#region SENSORS
public class EVENT_SENSOR_BROADCAST : GameEvent
{
    public SensorData sensorData;
    public EVENT_SENSOR_BROADCAST(SensorData _sensorData)
    {
        sensorData = _sensorData;
    }
}


#endregion

public class EventsList : MonoBehaviour { }
