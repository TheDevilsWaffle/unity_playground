using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickuper : SensorObject
{
    [SerializeField] KeyCode interact = KeyCode.E;
    [SerializeField] Transform pickupTransform;
    Transform environment;
    List<GameObject> items;
    GameObject currentItem;
    AttachmentPoint attachmentPoint;
    

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        items = new List<GameObject>();
        environment = GameObject.Find("Environment").gameObject.transform;    
    }

    public override void Activate(SensorData _sensorData)
    {
        if(_sensorData.detectee.tag == "Item")
        {
            if(_sensorData.status == SensorStatus.ENTERED || _sensorData.status == SensorStatus.PERSISTS && !CheckIfItemInList(_sensorData.detectee))
            {
                items.Add(_sensorData.detectee);
            }
            else if(_sensorData.status == SensorStatus.EXITED && CheckIfItemInList(_sensorData.detectee))
            {
                items.Remove(_sensorData.detectee);
            }
        }
        if(_sensorData.detectee.tag == "AttachmentPoint")
        {
            if(_sensorData.status == SensorStatus.ENTERED)
            {
                attachmentPoint = _sensorData.detectee.GetComponent<AttachmentPoint>();
            }
            else if(_sensorData.status == SensorStatus.EXITED)
            {
                attachmentPoint = null;
            }
        }
    }

    bool CheckIfItemInList(GameObject _go)
    {
        if(items.Contains(_go))
            return true;
        else
            return false;
    }

    void PickupItem()
    {
        if(items.Count >= 1)
        {
            currentItem = items[0];
            items.Remove(items[0]);
            currentItem.GetComponent<Item>().Status = ItemStatus.CARRIED;
            TogglePhysics(currentItem, true);
            currentItem.transform.SetParent(pickupTransform, true);
            currentItem.transform.localPosition = Vector3.zero;
            currentItem.transform.localRotation = Quaternion.Euler(0,0,0);
            
        }
    }
    void DropItem()
    {
        if(currentItem != null)
        {
            currentItem.GetComponent<Item>().Status = ItemStatus.PICKUPABLE;
            TogglePhysics(currentItem, false);
            currentItem.transform.SetParent(environment, true);
            currentItem.GetComponent<Rigidbody>().isKinematic = false;
            currentItem = null;
        }
    }
    public void PlaceItemOnAttachmentPoint()
    {
        currentItem.GetComponent<Item>().Status = ItemStatus.PICKUPABLE;
        attachmentPoint.AttachObject(currentItem);
        currentItem = null;
    }
    void TogglePhysics(GameObject _go, bool _active)
    {
        if(_go.GetComponent<Rigidbody>())
            _go.GetComponent<Rigidbody>().isKinematic = _active;
        if(_go.GetComponent<BoxCollider>())
            _go.GetComponent<BoxCollider>().isTrigger = _active;
        else if(_go.GetComponent<CapsuleCollider>())
            _go.GetComponent<CapsuleCollider>().isTrigger = _active;
        else if(_go.GetComponent<SphereCollider>())
            _go.GetComponent<SphereCollider>().isTrigger = _active;
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(interact))
        {
            if(currentItem == null && attachmentPoint != null && attachmentPoint.Status == AttachmentStatus.OCCUPIED)
            {
                attachmentPoint.Status = AttachmentStatus.EMPTY;
                PickupItem();
            }
            else if(currentItem == null)
            {
                PickupItem();
            }
            else if(currentItem != null && attachmentPoint != null)
            {
                if(attachmentPoint.Status == AttachmentStatus.EMPTY)
                {
                    PlaceItemOnAttachmentPoint();
                }
            }
            else if (currentItem != null)
            {
                DropItem();
            }
        }
    }
}
