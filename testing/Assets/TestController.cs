using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementStatus
{
    MAXSPEED,
    ACCELERATING,
    DECELERATING,
    STOPPED
};

public class TestController : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] KeyCode right = KeyCode.W;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] AnimationCurve accelerationCurve;
    [SerializeField] float accelerationRate = 5f;
    [SerializeField] AnimationCurve decelerationCurve;
    [SerializeField] float decelerationRate = 1f;
    
    MovementStatus movementStatus= MovementStatus.STOPPED;
    float currentVelocity;
    float currentAccelerationTime = 0;
    float currentDecelerationTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        currentVelocity = 0;
        movementStatus = MovementStatus.STOPPED;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(right))
        {
            Move();
        }
        else
        {
            Slow();
        }
    }
    void Move()
    {
        //stop decelerating
        currentDecelerationTime = 0;

        //start accelerating
        currentAccelerationTime += Time.deltaTime;
        currentVelocity = maxSpeed * accelerationCurve.Evaluate(currentAccelerationTime/accelerationRate);

        //ensure we do not go over maxSpeed
        if(currentVelocity >= maxSpeed)
        {
            currentVelocity = maxSpeed;
            movementStatus = MovementStatus.MAXSPEED;
        }
        else
        {
            movementStatus = MovementStatus.ACCELERATING;
        }
    }
    void Slow()
    {
        //stop accelerating
        currentAccelerationTime = 0;

        //start decelerating
        currentDecelerationTime += Time.deltaTime;
        currentVelocity -= decelerationCurve.Evaluate(currentDecelerationTime/decelerationRate);
        
        //ensure we do not go backwards
        if(currentVelocity <= 0)
        {
            currentVelocity = 0;
            movementStatus = MovementStatus.STOPPED;
        }
        else
        {
            movementStatus = MovementStatus.DECELERATING;
        }
    }
    void LateUpdate()
    {
        rigidbody.velocity = transform.forward * currentVelocity;
        print(currentVelocity + " | " + movementStatus);
    }
}
