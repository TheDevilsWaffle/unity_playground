using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] TMP_Text currentInventoryText;
    

    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate()
    {
        currentInventoryText.text = "empty";
    }
    void Awake()
    {
        OnValidate();
        SetSubscriptions();
    }
    #region SUBSCRIPTIONS
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_UI_INVENTORY_UPDATE>(UpdateInventory);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// RemoveSubscriptions()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void RemoveSubscriptions()
    {
        Events.instance.RemoveListener<EVENT_UI_INVENTORY_UPDATE>(UpdateInventory);
    }
    #endregion
    #region METHODS
    void UpdateInventory(EVENT_UI_INVENTORY_UPDATE _event)
    {
        if(_event.player == player)
        {
            if(_event.item != null)
                currentInventoryText.text = _event.item.name;
            else
            {
                currentInventoryText.text = "empty";
            }
        }
    }
    #endregion
    #region DESTROY
    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        RemoveSubscriptions();
    }
    #endregion
}
