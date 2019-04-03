/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// AttachmentPoint.cs
/// GOALS OF THIS SCRIPT
/// 1 -holds an item
/// 2 -item is placed centered upon an attachment point (not clipping with this)
/// 3 -item can be taken/replaced as long as it is not currently holding an item
/*///////////////////////////////////////////////////////////////////////////////////////////*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttachmentStatus
{
    OCCUPIED,
    EMPTY,
    DISABLED
};

public class AttachmentPoint : MonoBehaviour
{
    #region PROPERTIES
    [SerializeField] AttachmentStatus status = AttachmentStatus.EMPTY;
    public AttachmentStatus Status
    {
        get { return status; }
        set { status = value; }
    }
    GameObject objectHeld;
    public GameObject ObjectHeld
    {
        get { return objectHeld; }
        private set { objectHeld = value; }
    }
    [SerializeField] Vector3 offset = new Vector3(0, 5f, 0);
    [SerializeField] string[] targetTags;
    Transform tr;
    #endregion
    #region INITIALIZE
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// Awake()
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        tr = this.GetComponent<Transform>();
    }
    #endregion
    #region METHODS
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// AttachObject
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public void AttachObject(GameObject _go)
    {
        Status = AttachmentStatus.OCCUPIED;
        ObjectHeld = _go;
        ObjectHeld.transform.SetParent(tr, true);
        
        //not really needed if the scale of the item platform isn't stupid. Keeping for later just in case
        //ApplyScaleRatio();
        
        ObjectHeld.transform.localPosition = Vector3.zero + new Vector3(0 ,_go.GetComponent<Renderer>().bounds.size.y, 0) + offset;
        ObjectHeld.transform.localRotation = Quaternion.Euler(0,0,0);
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// DetachObject
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public void DetachObject()
    {
        Status = AttachmentStatus.EMPTY;
        ObjectHeld = null;
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// ApplyScaleRatio
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void ApplyScaleRatio()
    {
        Vector3 _ratio = Vector3.Scale(ObjectHeld.GetComponent<Item>()._OriginalLocalScale, tr.localScale);
        ObjectHeld.transform.localScale = _ratio;
        print ("original scale = " + ObjectHeld.GetComponent<Item>()._OriginalLocalScale);
        print ("scale of attachment = " + tr.localScale);
        print ("ratio = " + _ratio);
        print ("new scale of object = " + ObjectHeld.transform.localScale);
    }
    #endregion
}
