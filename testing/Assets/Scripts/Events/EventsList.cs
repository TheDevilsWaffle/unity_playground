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

public class EventsList : MonoBehaviour { }
