/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — Item
/// PURPOSE — generic item that is the base class for something that is able to be picked up
///           by either the player or other characters.
/*///////////////////////////////////////////////////////////////////////////////////////////*/

using UnityEngine;

public enum ItemStatus
{
    PICKUPABLE,
    CARRIED
};

public class Item : MonoBehaviour
{
    #region PROPERTIES
    GameObject owner;
    public GameObject _Owner
    {
        get { return owner; }
        set { owner = value; }
    }
    [SerializeField] ItemStatus status = ItemStatus.PICKUPABLE;
    public ItemStatus _Status
    {
        get { return status; }
        set { status = value; }
    }

    Vector3 originalWorldScale;
    public Vector3 _OriginalWorldScale
    {
        get { return originalWorldScale; }
        private set { originalWorldScale = value; }
    }

    Vector3 originalLocalScale;
    public Vector3 _OriginalLocalScale
    {
        get { return originalLocalScale; }
        private set { originalLocalScale = value; }
    }
    #endregion

    #region INITIALIZE
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called upon initialization and can be used to set values of members of this object
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public virtual void Awake()
    {
        //capture this in case we need this for weird scale issues when things are not (1,1,1)
        originalWorldScale = this.gameObject.transform.lossyScale;
        originalLocalScale = this.gameObject.transform.localScale;
    }
    public virtual void UseItem() { }
    #endregion
}