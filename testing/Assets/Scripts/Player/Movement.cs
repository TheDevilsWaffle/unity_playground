/*///////////////////////////////////////////////////////////////////////////////////////////*/
/// SCRIPT — Movement
/// PURPOSE — general 8 directional based movement
///           * accelerate/decelerate based on an animation curve
///           * uses unity's basic input sytem (keyboard)
///           * add xinput based input system once it is complete
/*///////////////////////////////////////////////////////////////////////////////////////////*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region PROPERTIES
    [Header("Camera")]
    [SerializeField] Transform mainCamera;
    [Header("Acceleration")]
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] AnimationCurve accelerationCurve;
    [SerializeField] float timeToMaxSpeed = 2f;
    [Header("Deceleration")]
    [SerializeField] AnimationCurve decelerationCurve;
    [SerializeField] float timeToZeroSpeed = 0.5f;
    [Header("Rotation")]
    [SerializeField] float turnSpeed = 10f;
    
    float currentSpeed = 0;
    float accelerationTimer = 0;
    float decelerationTimer = 0;
    float angle;
    Vector2 input;
    Quaternion targetRotation;
    Rigidbody rb;
    #endregion

    #region INITIALIZATION
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called when the script instance is being loaded
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    #endregion

    #region UPDATE
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// update
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        UpdateInput();
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// reliable physics update
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void FixedUpdate()
    {
        CalculateCurrentSpeed();
        Rotate();
        Move();
    }
    #endregion

    #region METHODS
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// get input, calcuate where we are going
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void UpdateInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        //if we've got input, continue and calculate direction. Otherwise, end here.
        if(Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;

        CalculateDirection();
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// which way are we going
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += mainCamera.eulerAngles.y;
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// rotatation
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// actually move (several types of movement to choose from (read directions))
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Move()
    {
        //move w/o physics
        //transform.position += transform.forward * currentSpeed * Time.deltaTime;
        //print("move = " + transform.forward * maxSpeed * Time.deltaTime);

        //move w/ physics, set velocity directly
        //rb.velocity = transform.forward.normalized * currentSpeed * Time.deltaTime;
        //print("velocity = " + transform.forward.normalized * currentSpeed);

        //move w/ physics, smooth movement:
        /*
        https://docs.unity3d.com/ScriptReference/Rigidbody.MovePosition.html
            If Rigidbody interpolation is enabled on the Rigidbody, calling Rigidbody.
            MovePosition results in a smooth transition between the two positions in any 
            intermediate frames rendered. This should be used if you want to continuously 
            move a rigidbody in each FixedUpdate.

            For this to work properly, the character's rigidbody needs to have:
            isKinematic: ?
            Interpolate: none
            Collision Detection: Continuous
         */
        rb.MovePosition(transform.position + (transform.forward.normalized * currentSpeed * Time.fixedDeltaTime));
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// return how fast/slow we are going
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void CalculateCurrentSpeed()
    {
        if(input.y != 0 || input.x != 0)
        {
            //print("accelerating");
            decelerationTimer = 0;
            accelerationTimer += Time.deltaTime;
            currentSpeed = maxSpeed * accelerationCurve.Evaluate(accelerationTimer/timeToMaxSpeed);
        }
        else if(input.y == 0 && input.x == 0)
        {
            //print("decelerating");
            accelerationTimer = 0;
            decelerationTimer += Time.deltaTime;

            currentSpeed -= decelerationCurve.Evaluate(decelerationTimer/timeToZeroSpeed);
        }
        if(currentSpeed > maxSpeed)
        {
            //print("max speed achieved");
            currentSpeed = maxSpeed;
        }
        else if(currentSpeed < 0)
        {
            //print("zero speed");
            currentSpeed = 0;
        }
        //print("current speed = " + currentSpeed);
    }
    #endregion
}
