/*////////////////////////////////////////////////////////////////////////////////////////////////*/
/// <summary>
/// SensorObject.cs
/// </summary>
/*///////////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SensorObject : MonoBehaviour
{
    #region PROPERTIES
    [SerializeField] ActivationType activationType = ActivationType.MANUAL;
    [Header("IF MANUAL")]
    [SerializeField] KeyCode activateKey = KeyCode.E;
    Interactable currentManualInteractable;
    #endregion
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (activationType == ActivationType.MANUAL && Input.GetKeyDown(activateKey))
            AttemptManualActivate();
    }
    void AttemptManualActivate()
    {
        if (currentManualInteractable != null)
            currentManualInteractable.Activate(this.gameObject);
    }
    #region VIRTUAL METHODS
    public virtual void Activate(SensorData _sensorData) { }
    public virtual void ManuallyActivateEnter(SensorData _sensorData) { }
    public virtual void ManuallyActivatePersists(SensorData _sensorData) { }
    public virtual void ManuallyActivateExit(SensorData _sensorData) { }
    public virtual void AutomaticActivateEnter(SensorData _sensorData) { }
    public virtual void AutomaticActivatePersists(SensorData _sensorData) { }
    public virtual void AutomaticActivateExit(SensorData _sensorData) { }
    #endregion

    #region METHODS
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// SensorAlert()
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    public void SensorAlert(SensorData _sd)
    {
        
    }
    #endregion
}
