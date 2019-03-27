/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// PowerSource.cs
/*///////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerType
{
    BATTERY,
    GENERATOR
};

public class PowerSource : MonoBehaviour
{
    #region PROPERTIES
    [SerializeField] PowerType type = PowerType.BATTERY;
    public PowerType _Type
    {
        get { return type; }
        set { type = value; }
    }
    [SerializeField] float currentCharge = 100f;
    public float _CurrentCharge
    {
        get { return currentCharge; }
        set { 
                currentCharge = value;
                chargePercentage = currentCharge / capacity;
            }
    }
    [SerializeField] float capacity = 100f;
    public float _Capacity
    {
        get { return capacity; }
        set { capacity = value; }
    }
    [Range(0, 1f)]
    [SerializeField] float chargePercentage = 1f;
    public float _ChargePercentage
    {
        get { return chargePercentage; }
        set { 
                chargePercentage = value; 
                currentCharge = chargePercentage * capacity;    
            }
    }
    [SerializeField] float dischargeTickRate = 1f;
    public float _DischargeTickRate
    {
        get { return dischargeTickRate; }
        set { dischargeTickRate = value; }
    } 
    [SerializeField] List<PoweredObject> poweredObjects;
    public List<PoweredObject> _PoweredObjects
    {
        get { return poweredObjects; }
        set { poweredObjects = value; }
    }
    float timer = 0f;
    #endregion
    #region INITIALIZATION
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Awake()
    /// </summar>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //set values
        timer = 0f;    
    }
    #endregion
    #region METHODS
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// ConnectPoweredObject
    /// </summar>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public void ConnectPoweredObject(PoweredObject _po)
    {
        poweredObjects.Add(_po);
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// DisconnectPoweredObject
    /// </summar>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public void DisconnectPoweredObject(PoweredObject _po)
    {
        if(poweredObjects.Contains(_po))
        {
            poweredObjects.Remove(_po);
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// DiscconectAllPoweredObjects()
    /// </summar>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public void DisconnectAllPoweredObjects()
    {
        poweredObjects.Clear();
        poweredObjects = new List<PoweredObject>();
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Discharge()
    /// </summar>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public void Discharge(float _value)
    {
        _ChargePercentage = chargePercentage - _value;
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// DebugLogValues()
    /// </summar>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public void DebugLogValues()
    {
        print("capacity = " + _Capacity);
        print("currentCharge = " + _CurrentCharge);
        print("chargePercentage = " + _ChargePercentage);
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// HasPoweredObjectsAttached()
    /// </summar>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    bool HasPoweredObjectsAttached()
    {
        if(poweredObjects.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// IsTimeToDischarge()
    /// </summar>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    bool IsTimeToDischarge()
    {
        timer += Time.deltaTime % 60;
        if(timer >= dischargeTickRate)
        {
            timer = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
    #region UPDATE
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Update()
    /// </summar>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public virtual void Update()
    {
        if(HasPoweredObjectsAttached())
        {
            if(IsTimeToDischarge())
            {
                foreach (PoweredObject _po in poweredObjects)
                {
                    Discharge(_po._DrainRatePercentage);
                    DebugLogValues();
                }
            }
        }
    }
    #endregion
}
