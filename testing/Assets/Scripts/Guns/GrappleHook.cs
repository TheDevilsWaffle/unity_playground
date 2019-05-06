/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — GrappleHook
/// PURPOSE — aim a laser for the grapple hook so player knows where they are targeting, then
///           (if possible), fire grapple and pull player towards location
/*///////////////////////////////////////////////////////////////////////////////////////////*/

using UnityEngine;

public enum GrappleHookStatus
{
    INACTIVE,
    AIMING,
    PULLING
};

public class GrappleHook : Gun
{
    #region PROPERTIES
    GrappleHookStatus grappleStatus = GrappleHookStatus.INACTIVE;
    [SerializeField] LineRenderer lr;
    [SerializeField] float aimDistance = 25f;
    float distanceToTarget = 0f;

    [SerializeField] AnimationCurve ac;
    Vector3 targetPosition;
    #endregion

    #region UPDATE
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void FixedUpdate()
    {
        //aim grapple hook
        if (grappleStatus == GrappleHookStatus.AIMING)
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
        //pull character towards point
        else if (grappleStatus == GrappleHookStatus.PULLING)
        {
            MoveOwner();
            DrawLine();

            //reset grapple hook
            if (distanceToTarget <= 1)
            {
                ResetGrapple();
                ResetOwnerVelocity();
            }
        }
    }
    #endregion

    #region METHODS
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// depending on status, either aim or fire the grapple hook
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    public override void Shoot()
    {
        if (grappleStatus == GrappleHookStatus.AIMING)
            FireGrappleHook();
        else if (grappleStatus == GrappleHookStatus.INACTIVE)
            grappleStatus = GrappleHookStatus.AIMING;
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// fire the grapple hook
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void FireGrappleHook()
    {
        //note: this could be a problem if the target is actually at (0,0,0)
        if (targetPosition != Vector3.zero)
        {
            lr.enabled = true;
            grappleStatus = GrappleHookStatus.PULLING;
            UpdateDistanceToTarget();
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// update distanceToTarget so we know how close we are getting to grapple hook point
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void UpdateDistanceToTarget()
    {
        distanceToTarget = Vector3.Distance(_Owner.transform.position, targetPosition);
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// reset the grapple status and timer so we can fire again
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void ResetGrapple()
    {
        grappleStatus = GrappleHookStatus.INACTIVE;
        lr.enabled = false;
        timer = 0;
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// don't carry over momentum from the pull (this could be fun though if we didn't do this)
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void ResetOwnerVelocity()
    {
        _Owner.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// move owner towards grapple hook spot
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void MoveOwner()
    {
        timer += Time.deltaTime;
        _Owner.transform.position = Vector3.MoveTowards(_Owner.transform.position, targetPosition, ac.Evaluate(timer * (Time.deltaTime * _Speed)));
        UpdateDistanceToTarget();
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// draw grapple hook line (more work needs to be done here)
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////*/
    void DrawLine()
    {
        lr.SetPosition(0, _FiringPoint.position);
        lr.SetPosition(1, targetPosition);
    }
    #endregion
}
