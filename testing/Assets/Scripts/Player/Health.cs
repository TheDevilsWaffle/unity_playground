/*////////////////////////////////////////////////////////////////////////////////////////////////*/
/// <summary>
/// Health.cs
/// </summary>
/*///////////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region PROPERTIES
    [SerializeField] float maxHealth = 10f;
    public float _MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    [SerializeField] float currentHealth = 10f;
    public float _CurrentHealth
    {
        get { return currentHealth; }
        set { UpdateCurrentHealth(value); }
    }
    [Range(0, 1f)]
    float currentHealthPercentage = 0;
    [SerializeField] GameObject healthBarPrefab;
    GameObject healthBar = null;
    [SerializeField] RectTransform uiCanvasRectTransform;    
    #endregion
    #region INITIALIZATION
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    void OnValidate()
    {
        UpdateCurrentHealth(currentHealth);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        OnValidate();
        SetSubscriptions();
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        GenerateHealthBar();
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// description
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_TARGET_UPDATE_HEALTH>(UpdateHealthEvent);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// description
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void RemoveSubscriptions()
    {
        Events.instance.RemoveListener<EVENT_TARGET_UPDATE_HEALTH>(UpdateHealthEvent);
    }    
    #endregion
    #region METHODS
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// GenerateHealthBar
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void GenerateHealthBar()
    {
        healthBar = GameObject.Instantiate(healthBarPrefab) as GameObject;
        healthBar.GetComponent<HealthBar>().SetHealthBar(this.gameObject.transform, uiCanvasRectTransform, currentHealthPercentage);
        healthBar.transform.SetParent(uiCanvasRectTransform, false);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// description
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void UpdateHealthEvent(EVENT_TARGET_UPDATE_HEALTH _event)
    {
        if(_event.target = this.gameObject)
        {
            UpdateCurrentHealth(_event.value);
        }
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// description
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void UpdateCurrentHealth(float _value)
    {
        currentHealth += _value;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth < 0)
        {
            currentHealth = 0;
            DestroyOwner();
        }
        currentHealthPercentage = currentHealth / maxHealth;
        if(healthBar != null)
        {
            healthBar.GetComponent<HealthBar>().UpdateHealthBarValue(currentHealthPercentage);
        }
        //Debug.Log("updating health by "+ _value +" to "+ currentHealth);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// description
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void DestroyOwner()
    {
        print("bleh, i have died!");
    }    
    #endregion
    #region TESTING
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            UpdateCurrentHealth(1f);
        }
        if(Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            UpdateCurrentHealth(-2.5f);
        }
    }
        
    #endregion
    #region ONDESTROY
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    void OnDestroy()
    {
        RemoveSubscriptions();
    }    
    #endregion
}
