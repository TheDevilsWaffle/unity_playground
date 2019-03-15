/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// Item.cs
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
    [SerializeField] ItemStatus status = ItemStatus.PICKUPABLE;
    public ItemStatus Status
    {
        get { return status; }
        set { status = value; }
    }

    Vector3 originalWorldScale;
    public Vector3 OriginalWorldScale
    {
        get { return originalWorldScale; }
        private set { originalWorldScale = value; }
    }

    Vector3 originalLocalScale;
    public Vector3 OriginalLocalScale
    {
        get { return originalLocalScale; }
        private set { originalLocalScale = value; }
    }
    #endregion

    #region INITIALIZE
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called when the script instance is being loaded.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public Item(ItemStatus _status)
    {
        status = _status;
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called upon initialization and can be used to set values of members of this object
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        OriginalWorldScale = this.gameObject.transform.lossyScale;
        OriginalLocalScale = this.gameObject.transform.localScale;
    }
    #endregion
}