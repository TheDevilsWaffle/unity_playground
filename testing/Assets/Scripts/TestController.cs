using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    [SerializeField] Transform cam;

    [SerializeField] float maxSpeed = 10f;
    [SerializeField] AnimationCurve accelerationCurve;
    [SerializeField] float timeToMaxSpeed = 2f;
    [SerializeField] AnimationCurve decelerationCurve;
    [SerializeField] float timeToZeroSpeed = 0.5f;
    [SerializeField] float turnSpeed = 10f;
    Quaternion targetRotation;
    Rigidbody rb;

    float currentSpeed = 0;
    float accelerationTimer = 0;
    float decelerationTimer = 0;
    float angle;
    Vector2 input;
    
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called when the script instance is being loaded
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        AddSubscriptions();
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called before the first frame update
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// add GameEvent listeners
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void AddSubscriptions()
    {
        //Events.instance.AddListener<EVENT_KEYBOARD_KEY_BROADCAST>(UpdateInput);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// remove GameEvent listeners
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void RemoveSubscriptions()
    {
        //Events.instance.RemoveListener<EVENT_KEYBOARD_KEY_BROADCAST>(UpdateInput);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// listen to keyboad key broadcast game event
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void UpdateInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
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
        angle += cam.eulerAngles.y;
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// rotate
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// actually move
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Move()
    {
        //transform.position += transform.forward * moveSpeed * Time.deltaTime;
        rb.velocity = transform.forward * currentSpeed;
        //rb.AddForce(transform.forward * moveSpeed * Time.deltaTime);
        print(transform.forward);
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// description
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void CalculateCurrentSpeed()
    {
        if(input.y == 1 )
        {
            print("accelerating");
            decelerationTimer = 0;
            accelerationTimer += Time.deltaTime;
            currentSpeed = maxSpeed * accelerationCurve.Evaluate(accelerationTimer/timeToMaxSpeed);
        }
        else if(Mathf.Abs(input.y) < 1)
        {
            print("decelerating");
            accelerationTimer = 0;
            decelerationTimer += Time.deltaTime;

            currentSpeed -= decelerationCurve.Evaluate(decelerationTimer/timeToZeroSpeed);
        }
        if(currentSpeed > maxSpeed)
        {
            print("max speed achieved");
            currentSpeed = maxSpeed;
        }
        else if(currentSpeed < 0)
        {
            print("zero speed");
            currentSpeed = 0;
        }
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// late update
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        UpdateInput();
        CalculateCurrentSpeed();
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void FixedUpdate()
    {
        //no input? exit the update loop
        if(Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;
        CalculateDirection();
        Rotate();
        Move();
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////*/
    /// <summary>
    /// called when the MonoBehaviour will be destroyed
    /// </summary>
    /*///////////////////////////////////////////////////////////////////////////////////////////////*/
    void OnDestroy()
    {
        RemoveSubscriptions();
    }
}
