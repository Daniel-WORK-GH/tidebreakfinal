using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class ShipMover : MonoBehaviour
{
    public PlayerShipHandler controllingPlayer;

    public GameObject wheel;

    public Transform enterPosition;
    public float enterRange = 2;

    [Range(0.0001f, 10f)]
    public float idleStoppingPower = 1;
    [Range(0.0001f, 10f)]
    public float activeStoppingPower = 1;

    public float moveSpeed, maxSpeed;

    [Range(0.0001f, 10000f)]
    public float rotateSpeed;

    private Rigidbody rb;
    private Vector3 input;

    private Vector3 currentAcceleration;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        Vector3 velocity = Vector3.zero;

        float dirDot = currentAcceleration.magnitude < 0.1 ? 1 : Vector2.Dot(
            new Vector2(-transform.right.x, -transform.right.z).normalized,
            new Vector2(currentAcceleration.x, currentAcceleration.z).normalized
        );

        bool sailing = false;

        if(controllingPlayer != null)
        {
            if(Input.GetKey(KeyCode.W))
            {
                velocity += -transform.right * moveSpeed;
                sailing = true;
            }
            else if(Input.GetKey(KeyCode.S))
            {
                velocity = -currentAcceleration * (Time.deltaTime * 1000) * activeStoppingPower;
            }

            if(Input.GetKey(KeyCode.A))
            {
                rb.AddTorque(transform.up * -rotateSpeed, ForceMode.Force);
                wheel.transform.Rotate(-12 * Time.deltaTime, 0, 0);
            }
            if(Input.GetKey(KeyCode.D))
            {
                rb.AddTorque(transform.up * rotateSpeed, ForceMode.Force);
                wheel.transform.Rotate(12 * Time.deltaTime, 0, 0);
            }

            if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                velocity = -currentAcceleration * (Time.deltaTime * 1000) * idleStoppingPower;
            }
        }
        else
        {
            velocity = -currentAcceleration * (Time.deltaTime * 1000) * idleStoppingPower;
        }


        Debug.Log("DOT = " + dirDot);
        Debug.Log("Accel = " + currentAcceleration);

        var acceleration = velocity / (Time.deltaTime * 1000);
        float currentSpeed = (currentAcceleration + acceleration).magnitude;
        float dotspeed = (dirDot * currentAcceleration + acceleration).magnitude;
        
        if(sailing)
        {
            if(currentSpeed > dotspeed)
            {
                currentAcceleration = dirDot * currentAcceleration + acceleration.normalized * (currentSpeed - dotspeed) + acceleration;
            }
            else 
            {
                currentAcceleration = dirDot * currentAcceleration + acceleration;
            }
        }
        else
        {
            dotspeed = (math.sqrt(dirDot) * currentAcceleration + acceleration).magnitude;
            currentAcceleration = math.sqrt(dirDot) * currentAcceleration - transform.right * (currentSpeed - dotspeed) * 0.9f + acceleration;
        }

        if(currentAcceleration.magnitude > maxSpeed)
        {
            currentAcceleration = currentAcceleration.normalized * maxSpeed;
        }

        rb.AddForce(currentAcceleration, ForceMode.Acceleration);
        
        if(controllingPlayer != null)
        {
            controllingPlayer.GetComponent<Rigidbody>().AddForce(currentAcceleration, ForceMode.Acceleration);
        }
    }

    public bool TryEnterShip(PlayerShipHandler player)
    {
        if(controllingPlayer == null)
        {
            if(Vector3.Distance(player.transform.position, enterPosition.position) < enterRange)
            {
                controllingPlayer = player;
                return true;
            }
        }
        return false;
    }
    
    public bool TryLeaveShip(PlayerShipHandler player)
    {
        if(controllingPlayer == player)
        {
            controllingPlayer = null;
            return true;
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green; 
        Gizmos.DrawWireSphere(enterPosition.position, enterRange);
    }
}
