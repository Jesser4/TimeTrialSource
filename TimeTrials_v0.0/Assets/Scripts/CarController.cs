using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class CarController : MonoBehaviour
{
    [Header("Car settings")]
    public float driftFactor = 0.9f;
    public float accelerationFactor = 13f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 17;
    
    //Local Variables
    float accelerationInput = 0;
    private float steeringInput = 0;
    private float rotationAngle = 0;
    private float velocityVsUp = 0;

    private Vector2 engineForceVector = Vector2.zero;
    
    // Components
    private Rigidbody2D carBody;

    // Called when script instance is being loaded.
    private void Awake()
    {
        carBody = GetComponent<Rigidbody2D>();
    }
    

    private void FixedUpdate()
    {
        ApplyEngineForce();

        KillSidewaysVelocity();
        
        ApplySteering();
    }

    void ApplyEngineForce()
    {

        velocityVsUp = Vector2.Dot(transform.up, carBody.velocity);

        //Forward Speed limit
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        //Reverse Speed limit
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }
        
        if (accelerationInput == 0)
        {
            carBody.drag = Mathf.Lerp(carBody.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            carBody.drag = 0;
        }
        engineForceVector = transform.up * accelerationInput * accelerationFactor;
        
        carBody.AddForce(engineForceVector, ForceMode2D.Force);
        
    }

    void ApplySteering()
    {

        float minSpeedBeforeTurn = carBody.velocity.magnitude / 8;
        minSpeedBeforeTurn = Mathf.Clamp01(minSpeedBeforeTurn);
        
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeTurn;
        
        carBody.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    void KillSidewaysVelocity()
    {
        //Get forward and right velocity of the car
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carBody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carBody.velocity, transform.right);

        //Kill the orthogonal velocity (side velocity) based on how much the car should drift. 
        carBody.velocity = forwardVelocity + rightVelocity * driftFactor;

    }
    

    public void SetRotation(int angle)
    {
        rotationAngle = angle;
    }
}
