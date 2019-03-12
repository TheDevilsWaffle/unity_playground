using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractiveStatus
{
    ENABLED,
    DISABLED
};

public class DoorControls : SensorObject
{
    [SerializeField] Door door;
    [SerializeField] InteractiveStatus iStatus = InteractiveStatus.ENABLED;
    public InteractiveStatus IStatus
    {
        get { return iStatus; }
        set { iStatus = value; }
    }

    public override void Activate(SensorData _sensorData)
    {
        print("here");
        if(IStatus == InteractiveObjectStatus.ENABLED)
        {
            if(!door.IsAutomatic)
            {
                if(door.Status == DoorStatus.CLOSE)
                {
                    door.UpdateDoor(DoorStatus.OPEN);
                }
                else if(door.Status == DoorStatus.OPEN)
                {
                    door.UpdateDoor(DoorStatus.CLOSE);
                }
            }
        }
    }
}
