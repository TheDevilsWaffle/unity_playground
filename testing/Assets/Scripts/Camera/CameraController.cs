using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offsetPos;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float smoothSpeed = 0.5f;

    Quaternion targetRotation;
    Vector3 targetPos;
    bool smoothRotating = false;

    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called when the script instance is being loaded.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        AddSubscriptions();
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// add GameEvent listeners
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void AddSubscriptions()
    {
        //Events.instance.AddListener<EVENT_KEYBOARD_KEY_BROADCAST>(UpdateKeyboardInput);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// remove GameEvent listeners
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void RemoveSubscriptions()
    {
        //Events.instance.RemoveListener<EVENT_KEYBOARD_KEY_BROADCAST>(UpdateKeyboardInput);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called every frame after all other update functions have been called.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void FixedUpdate()
    {
        MoveWithTarget();
        LookAtTarget();

        if(Input.GetKeyDown(KeyCode.G) && !smoothRotating)
        {
            StartCoroutine("RotateAroundTarget", 45);
        }
        if(Input.GetKeyDown(KeyCode.H) && !smoothRotating)
        {
            StartCoroutine("RotateAroundTarget", -45);
        }
            
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// move the camera to target position + offset (modified by RotateAroundTarget coroutine)
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void MoveWithTarget()
    {
        targetPos = target.position + offsetPos;
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// use the lookat vector (target - current) to aim camera toward the target
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void LookAtTarget()
    {
        targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// coroutine can only have one instance running at a time (determined by smoothRotating)
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    IEnumerator RotateAroundTarget(float _angle)
    {
        Vector3 vel = Vector3.zero;
        Vector3 targetOffsetPos = Quaternion.Euler(0, _angle, 0) * offsetPos;
        float dist = Vector3.Distance(offsetPos, targetOffsetPos);
        smoothRotating = true;

        while (dist < 0.02f)
        {
            offsetPos = Vector3.SmoothDamp(offsetPos, targetPos, ref vel, smoothSpeed);
            dist = Vector3.Distance(offsetPos, targetOffsetPos);
            yield return null;
        }

        smoothRotating = false;
        offsetPos = targetOffsetPos;
    }
    #region ONDESTROY
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        RemoveSubscriptions();
    }
    #endregion
}
