/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// ItemPickuper.cs
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
            if (AttemptPickupItem()) return;
            else AttemptDropItem();
        }
        if (Input.GetKeyDown(useItem))
        {
            AttemptUseItem();
        }

    }
    #endregion

    #region METHODS
    void AttemptUseItem()
    {
        if (currentItem != null)
        {
            currentItem.GetComponent<Item>().UseItem();
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// Activate
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public override void Activate(SensorData _sensorData)
    {
        if (_sensorData.detectee.tag == "Item")
        {
            if (_sensorData.status == SensorStatus.ENTERED || _sensorData.status == SensorStatus.PERSISTS && !IsItemInList(_sensorData.detectee))
            {
                items.Add(_sensorData.detectee);
            }
            else if (_sensorData.status == SensorStatus.EXITED && IsItemInList(_sensorData.detectee))
            {
                items.Remove(_sensorData.detectee);
            }
        }
        if (_sensorData.detectee.tag == "AttachmentPoint")
        {
            if (_sensorData.status == SensorStatus.ENTERED)
            {
                attachmentPoint = _sensorData.detectee.GetComponent<AttachmentPoint>();
            }
            else if (_sensorData.status == SensorStatus.EXITED)
            {
                attachmentPoint = null;
            }
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// IsItemInList
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    bool IsItemInList(GameObject _go)
    {
        if (items.Contains(_go))
            return true;
        else
            return false;
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// PickupItem
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void PickupItem(bool _worldScalingStays = true)
    {
        if (items.Count >= 1)
        {
            currentItem = items[0];
            items.Remove(items[0]);
            currentItem.GetComponent<Item>()._Status = ItemStatus.CARRIED;
            TogglePhysics(currentItem, true);
            // currentItem.transform.localScale = currentItem.GetComponent<Item>().OriginalWorldScale;
            // print("current" + currentItem.transform.localScale);
            // print("stored" + currentItem.GetComponent<Item>().OriginalWorldScale);
            currentItem.transform.SetParent(pickupTransform, _worldScalingStays);
            currentItem.transform.localPosition = Vector3.zero;
            currentItem.transform.localRotation = Quaternion.Euler(0, 0, 0);

            UpdatePlayerInventoryUI(currentItem);
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// DropItem
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void DropItem()
    {
        if (currentItem != null)
        {
            currentItem.GetComponent<Item>()._Status = ItemStatus.PICKUPABLE;
            TogglePhysics(currentItem, false);
            currentItem.transform.SetParent(environment, true);
            currentItem.transform.localScale = currentItem.GetComponent<Item>()._OriginalWorldScale;
            currentItem.GetComponent<Rigidbody>().isKinematic = false;
            currentItem = null;

            UpdatePlayerInventoryUI(null);
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// PlaceItemOnAttachmentPoint
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public void PlaceItemOnAttachmentPoint()
    {
        currentItem.GetComponent<Item>()._Status = ItemStatus.PICKUPABLE;
        attachmentPoint.AttachObject(currentItem);
        currentItem = null;

        UpdatePlayerInventoryUI(null);
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// TogglePhysics
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void TogglePhysics(GameObject _go, bool _active)
    {
        //run through the guantlet of rigidbody & collider types
        if (_go.GetComponent<Rigidbody>())
        {
            _go.GetComponent<Rigidbody>().isKinematic = _active;
        }
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
    /// AttemptPickupItem
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    bool AttemptPickupItem()
    {
        if (currentItem == null && attachmentPoint != null && attachmentPoint.Status == AttachmentStatus.OCCUPIED)
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
    /// AttemptDropItem
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void AttemptDropItem()
    {
        if (currentItem != null && attachmentPoint != null)
        {
            if (attachmentPoint.Status == AttachmentStatus.EMPTY)
            {
                PlaceItemOnAttachmentPoint();
            }
        }
        else if (currentItem != null)
        {
            DropItem();
        }
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// UpdatePlayerInventoryUI
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void UpdatePlayerInventoryUI(GameObject _item)
    {
        Events.instance.Raise(new EVENT_UI_INVENTORY_UPDATE(this.gameObject, _item));
    }
    #endregion
}
