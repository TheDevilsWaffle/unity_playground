﻿using System.Collections;
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

#region INPUT
public class EVENT_PLAYER_INPUT_BROADCAST : GameEvent
{
    public GameObject player;
    public KeyCode key;
    public EVENT_PLAYER_INPUT_BROADCAST(GameObject _player, KeyCode _key)
    {
        player = _player;
        key = _key;
    }
}
    
#endregion

public class EventsList : MonoBehaviour { }
