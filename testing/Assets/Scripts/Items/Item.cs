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
        originalWorldScale = this.gameObject.transform.lossyScale;
        originalLocalScale = this.gameObject.transform.localScale;
    }
    public virtual void UseItem() { }
    #endregion
}