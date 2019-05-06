/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — KineticGun
/// PURPOSE — standard physics based, projectile firing gun
/*///////////////////////////////////////////////////////////////////////////////////////////*/

using UnityEngine;

public class KineticGun : Gun
{
    #region PROPERTIES
    [SerializeField] GameObject projectilePrefab;
    #endregion

    #region UPDATE
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //used for shoot delay/cooldown
        if (timer < _ShootDelay)
            timer += Time.deltaTime;
    }
    #endregion
    #region METHODS
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// if possible, shoot a projectile (instantiate and set velocity to projectile prefab)
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public override void Shoot()
    {
        if (CanFire())
        {
            ApplyVelocity(CreateProjectile());
            timer = 0; //reset shotdelay timer
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// add velocity to projectile
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void ApplyVelocity(GameObject _projectile)
    {
        _projectile.GetComponent<Rigidbody>().AddForce(_FiringPoint.forward.normalized * _Speed, ForceMode.Impulse);
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// returns whether or not gun can fire based on shoot delay/cooldown
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    bool CanFire()
    {
        if (timer >= _ShootDelay)
            return true;
        else
            return false;
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// create projectile based on prefab, place in front of gun
    /// TODO : projectile hierarchical placement in environment? object pooling? more here
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    GameObject CreateProjectile()
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab, (_FiringPoint.position + _FiringOffset), Quaternion.identity);
        return projectile;
    }
    #endregion
}
