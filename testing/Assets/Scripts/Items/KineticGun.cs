using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticGun : Gun
{
    [SerializeField] GameObject projectilePrefab;
    public override void Shoot()
    {
        if (CanFire())
        {
            ApplyVelocity(CreateProjectile());

            //reset shotdelay timer
            timer = 0;
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (timer < _ShootDelay)
        {
            timer += Time.deltaTime;
        }
    }
    GameObject CreateProjectile()
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab,
                                                       (_FiringPoint.position + _FiringOffset),
                                                       Quaternion.identity);
        return projectile;
    }
    void ApplyVelocity(GameObject _projectile)
    {
        _projectile.GetComponent<Rigidbody>().AddForce(_FiringPoint.forward.normalized * _Speed, ForceMode.Impulse);
    }
    bool CanFire()
    {
        if (timer >= _ShootDelay)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
