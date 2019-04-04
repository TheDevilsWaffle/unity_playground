using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrappleHookStatus
{
    INACTIVE,
    AIMING,
    PULLING
};

public class GrappleHook : Gun
{
    GrappleHookStatus status = GrappleHookStatus.INACTIVE;
    [SerializeField] LineRenderer lr;
    [SerializeField] float aimDistance = 25f;
    float distanceToTarget = 0f;

    [SerializeField] AnimationCurve ac;
    Vector3 targetPosition;

    public override void Shoot()
    {
        if (status == GrappleHookStatus.AIMING)
        {
            FireGrappleHook();
        }
        else if (status == GrappleHookStatus.INACTIVE)
        {
            status = GrappleHookStatus.AIMING;
        }
    }
    void FireGrappleHook()
    {
        if (targetPosition != Vector3.zero)
        {
            lr.enabled = true;
            status = GrappleHookStatus.PULLING;
            UpdateDistanceToTarget();
        }
    }
    void UpdateDistanceToTarget()
    {
        distanceToTarget = Vector3.Distance(_Owner.transform.position, targetPosition);
    }
    void ResetGrapple()
    {
        status = GrappleHookStatus.INACTIVE;
        lr.enabled = false;
        timer = 0;
    }
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (status == GrappleHookStatus.AIMING)
        {
            Ray ray = new Ray(_FiringPoint.position, _FiringPoint.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, aimDistance))
            {
                targetPosition = hit.point;
                Debug.DrawLine(_FiringPoint.position, _FiringPoint.position + _FiringPoint.forward * Vector3.Distance(_Owner.transform.position, targetPosition), Color.red);
            }
            else
            {
                targetPosition = Vector3.zero;
                Debug.DrawLine(_FiringPoint.position, _FiringPoint.position + _FiringPoint.forward * aimDistance, Color.red);
            }
        }
        else if (status == GrappleHookStatus.PULLING)
        {
            MoveOwner();
            DrawLine();
            if (distanceToTarget <= 1)
            {
                ResetGrapple();
                ResetOwnerVelocity();
            }
        }
    }
    void ResetOwnerVelocity()
    {
        _Owner.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    void MoveOwner()
    {
        timer += Time.deltaTime;
        _Owner.transform.position = Vector3.MoveTowards(_Owner.transform.position, targetPosition, ac.Evaluate(timer * (Time.deltaTime * _Speed)));
        UpdateDistanceToTarget();
    }

    void DrawLine()
    {
        lr.SetPosition(0, _FiringPoint.position);
        lr.SetPosition(1, targetPosition);
    }
}
