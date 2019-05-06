/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — ItemPickuper
/// PURPOSE — allow character to pick up an item as long as it is in their list of things they
///           can pick up. Also, allow thm to place item on attachment points or drop openly
/*///////////////////////////////////////////////////////////////////////////////////////////*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickuper : SensorObject
{
    #region PROPERTIES
    [Header("ITEM PICKUPER")]
    [SerializeField] KeyCode interact = KeyCode.E;
    [SerializeField] KeyCode useItem = KeyCode.Return;
    [SerializeField] Transform pickupTransform;

    Transform environment;
    List<GameObject> items;
    GameObject currentItem;
    AttachmentPoint attachmentPoint;
    #endregion

    #region INITIALIZATION
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called when the script instance is being loaded.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        items = new List<GameObject>();
        environment = GameObject.Find("Environment").gameObject.transform;
    }
    #endregion

    #region UPDATE
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if (Input.GetKeyDown(interact))
        {
            //attempt to pickup, if unable then stop, else try to drop something if possible
            if (AttemptPickupItem()) return;
            else AttemptDropItem();
        }
        //use the 'use key' to try and used a picked up item
        if (Input.GetKeyDown(useItem))
            AttemptUseItem();
    }
    #endregion

    #region METHODS
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// if we have an item, use it
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void AttemptUseItem()
    {
        if (currentItem != null)
            currentItem.GetComponent<Item>().UseItem();
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// Activate
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public override void Activate(SensorData _sensorData)
    {
        if (_sensorData.detectee.tag == "Item")
        {
            if (_sensorData.status == SensorStatus.ENTERED || _sensorData.status == SensorStatus.PERSISTS && !IsItemInList(_sensorData.detectee))
                items.Add(_sensorData.detectee);
            else if (_sensorData.status == SensorStatus.EXITED && IsItemInList(_sensorData.detectee))
                items.Remove(_sensorData.detectee);
        }
        if (_sensorData.detectee.tag == "AttachmentPoint")
        {
            if (_sensorData.status == SensorStatus.ENTERED)
                attachmentPoint = _sensorData.detectee.GetComponent<AttachmentPoint>();
            else if (_sensorData.status == SensorStatus.EXITED)
                attachmentPoint = null;
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// check to make sure the item is in our list of approved items
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    bool IsItemInList(GameObject _go)
    {
        if (items.Contains(_go))
            return true;
        else
            return false;
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// pick up the item, add to hierarchy, update ui
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void PickupItem(bool _worldScalingStays = true)
    {
        if (items.Count >= 1)
        {
            //add to array of items
            currentItem = items[0];

            //set attributes
            currentItem.GetComponent<Item>()._Status = ItemStatus.CARRIED;
            currentItem.GetComponent<Item>()._Owner = this.gameObject;
            TogglePhysics(currentItem, true);
            currentItem.transform.SetParent(pickupTransform, _worldScalingStays);
            currentItem.transform.localPosition = Vector3.zero;
            currentItem.transform.localRotation = Quaternion.Euler(0, 0, 0);

            //update ui
            UpdatePlayerInventoryUI(currentItem);
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// drops the current item, updates ui
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void DropItem()
    {
        if (currentItem != null)
        {
            //set attributes
            currentItem.GetComponent<Item>()._Status = ItemStatus.PICKUPABLE;
            currentItem.GetComponent<Item>()._Owner = null;
            TogglePhysics(currentItem, false);
            currentItem.transform.SetParent(environment, true);
            currentItem.transform.localScale = currentItem.GetComponent<Item>()._OriginalWorldScale;
            currentItem.GetComponent<Rigidbody>().isKinematic = false;
            currentItem = null;

            //update ui
            UpdatePlayerInventoryUI(null);
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// places item on an attachment point, updates ui
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public void PlaceItemOnAttachmentPoint()
    {
        //set that item able to be picked up again (this might change in future for items that are
        // meant to stay in one spot)
        currentItem.GetComponent<Item>()._Status = ItemStatus.PICKUPABLE;
        attachmentPoint.AttachObject(currentItem);
        currentItem = null;

        //update ui
        UpdatePlayerInventoryUI(null);
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// we don't want physics to get in the way if we're holding, and we want them back on afterwards
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void TogglePhysics(GameObject _go, bool _active)
    {
        //run through the guantlet of rigidbody & collider types
        if (_go.GetComponent<Rigidbody>())
            _go.GetComponent<Rigidbody>().isKinematic = _active;
        if (_go.GetComponent<BoxCollider>())
        {
            _go.GetComponent<BoxCollider>().isTrigger = _active;
            return;
        }
        else if (_go.GetComponent<SphereCollider>())
        {
            _go.GetComponent<SphereCollider>().isTrigger = _active;
            return;
        }
        else if (_go.GetComponent<CapsuleCollider>())
        {
            _go.GetComponent<CapsuleCollider>().isTrigger = _active;
            return;
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// try to pick up an item
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    bool AttemptPickupItem()
    {
        if (currentItem == null && attachmentPoint != null && attachmentPoint._Status == AttachmentStatus.OCCUPIED)
        {
            attachmentPoint.DetachObject();
            PickupItem();
            return true;
        }
        else if (currentItem == null)
        {
            PickupItem();
            return true;
        }
        return false;
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// try to drop an item
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void AttemptDropItem()
    {
        if (currentItem != null && attachmentPoint != null)
        {
            if (attachmentPoint._Status == AttachmentStatus.EMPTY)
                PlaceItemOnAttachmentPoint();
        }
        else if (currentItem != null)
            DropItem();
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// send message to ui of what we've got
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void UpdatePlayerInventoryUI(GameObject _item)
    {
        Events.instance.Raise(new EVENT_UI_INVENTORY_UPDATE(this.gameObject, _item));
    }
    #endregion
}
