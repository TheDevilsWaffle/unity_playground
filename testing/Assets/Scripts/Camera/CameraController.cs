/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — CameraController
/// PURPOSE — 3rd person camera
///           * follows player at distance based on 'offsetPosition'
///           * manually controllable using 'rotateLeft' and 'rotateRight'
/*///////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region PROPERTIES
    [Header("Target")]
    [SerializeField] Transform target;
    [SerializeField] Vector3 offsetPosition;
    [Header("Speeds")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 5f;
    [Header("Rotation Control")]
    [SerializeField] KeyCode rotateLeft = KeyCode.LeftArrow;
    [SerializeField] KeyCode rotateRight = KeyCode.RightArrow;
    [SerializeField] int rotationIncrement = 45;
    [SerializeField] float rotateSpeed = 0.5f;

    Vector3 targetPosition;
    bool isRotating = false;
    #endregion

    #region UPDATE
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called every frame after all other update functions have been called.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void FixedUpdate()
    {
        //move camera to target position + offset
        targetPosition = target.position + offsetPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        //aim camera toward the target
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if(Input.GetKeyDown(rotateLeft) && !isRotating)
            StartCoroutine("RotateAroundTarget", rotationIncrement);
        if(Input.GetKeyDown(rotateRight) && !isRotating)
            StartCoroutine("RotateAroundTarget", -rotationIncrement);
            
    }
    #endregion

    #region COROUTINES
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// manual rotation around target. Only one instance running at a time (determined by isRotating)
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    IEnumerator RotateAroundTarget(float _angle)
    {
        Vector3 velocity = Vector3.zero;
        Vector3 targetOffsetPosition = Quaternion.Euler(0, _angle, 0) * offsetPosition;
        float remainingDistance = Vector3.Distance(offsetPosition, targetOffsetPosition);
        isRotating = true;

        while (remainingDistance < 0.02f)
        {
            offsetPosition = Vector3.SmoothDamp(offsetPosition, targetPosition, ref velocity, rotateSpeed);
            remainingDistance = Vector3.Distance(offsetPosition, targetOffsetPosition);
            yield return null;
        }

        isRotating = false;
        offsetPosition = targetOffsetPosition;
    }
    #endregion
}
