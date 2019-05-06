/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — Gun
/// PURPOSE — base class for guns, inherits from item
/*///////////////////////////////////////////////////////////////////////////////////////////*/

using UnityEngine;

public class Gun : Item
{
    #region PROPERTIES
    [SerializeField] float damage;
    public float _Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    [SerializeField] float shootDelay;
    public float _ShootDelay
    {
        get { return shootDelay; }
        set { shootDelay = value; }
    }
    [SerializeField] float speed;
    public float _Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    [SerializeField] Transform firingPoint;
    public Transform _FiringPoint
    {
        get { return firingPoint; }
        set { firingPoint = value; }
    }
    [SerializeField] Vector3 firingOffset = new Vector3(0, 0, 0);
    public Vector3 _FiringOffset
    {
        get { return firingOffset; }
        set { firingOffset = value; }
    }
    protected float timer;
    #endregion

    #region METHODS
    public override void UseItem()
    {
        Shoot();
    }
    public virtual void Shoot() { }
    #endregion
}
