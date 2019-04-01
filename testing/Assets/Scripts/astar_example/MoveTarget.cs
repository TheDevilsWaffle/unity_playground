using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public LayerMask hitLayers;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition; //mouse pos
            Ray castPoint = Camera.main.ScreenPointToRay(mouse); //cast a ray to mouse pos
            RaycastHit hit; // used to store where the ray hit
            if(Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers)) //make sure we don't hit walls
            {
                this.transform.position = hit.point; //move the target to the mouse position
            }
        }
    }
}
