using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MaterialChanger))]
public class Battery : PowerSource
{
    [Range(0,1f)]
    [SerializeField] float[] levels = new float[2];
    MaterialChanger mc;
    // Start is called before the first frame update
    void Start()
    {
        mc = GetComponent<MaterialChanger>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
        if(_ChargePercentage < levels[0])
        {
            mc.ChangeMaterial(1);
        }
        if(_ChargePercentage < levels[1])
        {
            mc.ChangeMaterial(2);
        }
        // if(_ChargePercentage < levels[2])
        // {
        //     mc.ChangeMaterial(3);
        // }
    }
}
