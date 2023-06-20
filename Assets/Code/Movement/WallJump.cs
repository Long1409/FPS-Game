using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
    public LayerMask whatIsWall;
    public PlayerMov pm;
    public LedgeCrabbing lg;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;

    private bool climbing;

    [Header("WallJumping")]
    public float wallJumpUpForce;
    public float wallJumpdownForce;

    public KeyCode jumpkey = KeyCode.Space;
    public int climbJumps;
    private int climpJumpLeft;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    private Transform lastWall;
    private Vector3 lastWallNormal;
    public float minWallNormalAngelChange;

    [Header("Exiting")]
    public bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    private void Start()
    {
        lg = GetComponent<LedgeCrabbing>();
    }

    private void Update()
    {
        wallCheck();
        StateMachine();
        if (climbing && !exitingWall) ClimbingMovement();
    }

    private void StateMachine()
    {
        if (lg.holding || lg.exitingLedge)
        {
            if (climbing) StopClimbing();
        }

        else if(wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle && !exitingWall)
        {
            if (!climbing && climbTimer > 0) StartClimbing();

            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            if (climbTimer < 0) StopClimbing();
        }

        else if (exitingWall)
        {
            if (climbing) StopClimbing();

            if (exitWallTimer > 0) exitWallTimer -= Time.deltaTime;
            else exitingWall = false;
        }

        else
        {
            if (climbing) StopClimbing();
        }

        if (wallFront && Input.GetKeyDown(jumpkey) && climpJumpLeft > 0) wallJump();
    }

    private void wallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);

        bool newWall = frontWallHit.transform != lastWall || Mathf.Abs(Vector3.Angle(lastWallNormal, frontWallHit.normal)) > minWallNormalAngelChange;

        if ((wallFront && newWall) || pm.grounded)
        {
            climbTimer = maxClimbTime;
            climpJumpLeft = climbJumps;
        }
    }

    private void StartClimbing()
    {
        climbing = true;
        pm.climbing = true;

        lastWall = frontWallHit.transform;
        lastWallNormal = frontWallHit.normal;
    }

    private void ClimbingMovement()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
    }

    private void StopClimbing()
    {
        climbing = false;
        pm.climbing = false;    
    }

    private void wallJump()
    {
        if (pm.grounded) return;
        if (lg.holding || lg.exitingLedge) return;
        exitingWall = true;
        exitWallTimer = exitWallTime;
        Vector3 forceToApply = transform.up * wallJumpUpForce + frontWallHit.normal * wallJumpdownForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

        climpJumpLeft--;
    }
}
