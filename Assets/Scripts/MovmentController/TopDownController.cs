using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class TopDownController : MonoBehaviour
{



    //Player Controller variables
    public float speed = 5.0f;
    public float gravity = 14.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    private EventInstance playerFootsteps;
    //Private variables
    bool grounded = false;
    Rigidbody r;

    void Start()
    {
        playerFootsteps = AudioManager.instance.CreateInstance(FMODEvents.instance.playerFootsteps);
    }

    void Awake()
    {
        r = GetComponent<Rigidbody>();
        r.freezeRotation = true;
        r.useGravity = false;

        

    }



    public void Move(Vector3 dirction)
    {
        dir = dirction;
    }

    Vector3 dir;

    public void Jump()
    {
        IsJumpingThisFrame = true;
    }

    bool IsJumpingThisFrame = false;

    public bool IsGrounded()
    {
        return grounded;    
    }

    private void UpdateSound()
    {
        if (r.velocity.x != 0 && IsGrounded())
        {
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
            else
            {
                playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            Vector3 targetVelocity = dir;
            


            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = r.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            r.AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if (canJump && IsJumpingThisFrame)
            {
                r.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);

                IsJumpingThisFrame = false;
            }
            UpdateSound();
        }

        // We apply gravity manually for more tuning control
        r.AddForce(new Vector3(0, -gravity * r.mass, 0));
        
        grounded = false;

        
    }

    

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
    
}



// optional rectangle boundary
[System.Serializable]
public struct Boundary
{
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;
}

