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

    public override void UseItem()
    {
        Shoot();
    }
    public virtual void Shoot() { }
}
