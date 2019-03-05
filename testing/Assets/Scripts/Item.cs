using UnityEngine;

public enum ItemStatus
{
    PICKUPABLE,
    CARRIED
};

public class Item : MonoBehaviour
{
    [SerializeField] ItemStatus status = ItemStatus.PICKUPABLE;
    public ItemStatus Status
    {
        get { return status; }
        set { status = value; }
    }
    public Item(ItemStatus _status)
    {
        status = _status;
    }
}