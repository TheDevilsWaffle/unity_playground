/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// AttachmentPoint.cs
/// PURPOSE — allow a character to place or remove items onto specific points
///             1 -holds an item
///             2 -item is placed centered upon an attachment point (not clipping with this)
///             3 -item can be taken/replaced as long as it is not currently holding an item
/// TODO — use this to power doors/switches, unlock things (possibly puzzles?), etc.
/*///////////////////////////////////////////////////////////////////////////////////////////*/

using System.Collections;
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
    public AttachmentStatus _Status
    {
        get { return status; }
        set { status = value; }
    }
    GameObject objectHeld;
    public GameObject _ObjectHeld
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
    /// place an object on this
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public void AttachObject(GameObject _go)
    {
        _Status = AttachmentStatus.OCCUPIED;
        _ObjectHeld = _go;
        _ObjectHeld.transform.SetParent(tr, true);
        
        //not really needed if the scale of the item platform is (1,1,1). Keeping for later just in case
        //ApplyScaleRatio();
        
        _ObjectHeld.transform.localPosition = Vector3.zero + new Vector3(0 ,_go.GetComponent<Renderer>().bounds.size.y, 0) + offset;
        _ObjectHeld.transform.localRotation = Quaternion.Euler(0,0,0);
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// take the object 
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public void DetachObject()
    {
        _Status = AttachmentStatus.EMPTY;
        _ObjectHeld = null;
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// if an object placed onto an attachment point is not (1,1,1), weirdness can happen
    /// this attempts to resize an object based upon the ratio of it to this (not perfect, but works)
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void ApplyScaleRatio()
    {
        Vector3 _ratio = Vector3.Scale(_ObjectHeld.GetComponent<Item>()._OriginalLocalScale, tr.localScale);
        _ObjectHeld.transform.localScale = _ratio;
    }
    #endregion
}
