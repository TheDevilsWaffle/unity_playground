/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — BillboardTowardsCamera
/// PURPOSE — make sure UI element is always facing towards camera
/*///////////////////////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;
 
public class BillboardTowardsCamera : MonoBehaviour
{
    #region PROPERTIES
    [SerializeField] Camera mainCamera;
    Transform tr;
    #endregion
    
    #region INITIALIZATION
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// upon script enable
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        tr = GetComponent<Transform>();    

        if(mainCamera == null)
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    #endregion

    #region UPDATE
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// last update
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void LateUpdate()
    {
        tr.LookAt(tr.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
    #endregion
}