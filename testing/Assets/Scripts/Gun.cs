// "Create Summary Comment Block":
// 	{
// 		"prefix": "///",
// 		"body":
// 		[
// 			"/*////////////////////////////////////////////////////////////////////////////////////////////////*/",
// 			"/// <summary>",
// 			"/// $0${1:description}",
// 			"/// </summary>",
// 			"/*///////////////////////////////////////////////////////////////////////////////////////////////*/"
// 		],
// 		"description": "creates a unity serialize field property"
// 	}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
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
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firingPoint;
    [SerializeField] Vector3 firingOffset = new Vector3(0, 0, 0);
    
    float timer;

    public void Shoot()
    {
        if(CanFire())
        {
            ApplyVelocity(CreateProjectile());
            
            //reset shotdelay timer
            timer = 0;
        }
        
    }
    GameObject CreateProjectile()
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab,
                                                       (firingPoint.position + firingOffset),
                                                       Quaternion.identity);
        return projectile;
    }
    void ApplyVelocity(GameObject _projectile)
    {
        _projectile.GetComponent<Rigidbody>().AddForce(firingPoint.forward.normalized * speed, ForceMode.Impulse);
    }
    bool CanFire()
    {
        if(timer >= shootDelay)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Testing();
        
        if(timer < shootDelay)
        {
            timer += Time.deltaTime;
        }
    }
    void Testing()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Shoot();
        }
    }
}
