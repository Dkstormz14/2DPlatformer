using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 3f;
    public DetectionZone attackZone;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;

    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection;
    private Vector2 WalkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set {
            if (_walkDirection != value)
            {
                // Direction has changed, flip the sprite
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    WalkDirectionVector = Vector2.right;
                } else if (value == WalkableDirection.Left)
                {
                    WalkDirectionVector = Vector2.left;
                }
            }
            
            _walkDirection = value; }
    }

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get 
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         HasTarget = attackZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {

        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        rb.linearVelocity = new Vector2(walkSpeed * WalkDirectionVector.x, rb.linearVelocity.y);
    } 

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        } else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        } else
        {
            Debug.LogError("Current walkabale direction is ot set to legal values of right or left");
        }
    }
}
