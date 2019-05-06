/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — EventsList
/// PURPOSE — central location of all events used
/*///////////////////////////////////////////////////////////////////////////////////////////*/

using UnityEngine;

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

#region HEALTH
public class EVENT_TARGET_UPDATE_HEALTH : GameEvent
{
    public GameObject target;
    public GameObject source;
    public float value;
    public EVENT_TARGET_UPDATE_HEALTH(GameObject _target, GameObject _source, float _value)
    {
        source = _source;
        target = _target;
        value = _value;
    }
}
#endregion

#region UI—INVENTORY
public class EVENT_UI_INVENTORY_UPDATE : GameEvent
{
    public GameObject player;
    public GameObject item;
    public EVENT_UI_INVENTORY_UPDATE(GameObject _player, GameObject _item)
    {
        player = _player;
        item = _item;
    }
}
#endregion

public class EventsList : MonoBehaviour { }
