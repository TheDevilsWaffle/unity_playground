/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// PoweredObject.cs
/*///////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredObject : MonoBehaviour
{
    #region PROPERTIES
    [SerializeField] float drainRatePercentage = 0.0125f;
    public float _DrainRatePercentage
    {
        get { return drainRatePercentage; }
        set { drainRatePercentage = value; }
    }
    [SerializeField] List<PowerSource> powerSources = new List<PowerSource>();
    public List<PowerSource> _PowerSources
    {
        get { return powerSources; }
        set { powerSources = value; }
    }    
    #endregion
    #region METHODS
    public virtual void PowerOn() { }
    public virtual void PowerOff() { }
    #endregion
}
