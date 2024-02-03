using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///    The default movement option for 2D sections where acceleration and deceleration are
///    instantaneous. Currently not used.
/// </summary>
public class DefaultMovement2D : IMovementOption
{
    /// <summary>The rate at which the player's velocity increases</summary>
    const float ACCELERATION = 15f;
    /// <summary>The player's maximum speed</summary>
    const float MAX_SPEED = 10f;
    /// <summary>The amount of force exerted when the player is shot into the air via the jump command</summary>
    const float JUMP_FORCE = 7f;
    /// <summary>The amount of time the player can hold JUMP for to continue adding force</summary>
    int jumpTimer = 0;
    /// <summary>If the player has started a jump</summary>
    bool jumpInitiated = false;

    public void Move(float xMove, float y_move, bool isGrounded, bool jumpPressed, bool jumpHeld, Rigidbody rb) {
        // Get x-axis input and move character horizontally
        float xMag =  Mathf.Clamp(
            (xMove * ACCELERATION * Time.fixedDeltaTime) + rb.velocity.x,
            -MAX_SPEED,
            MAX_SPEED
            );
        // Apply gravity if the player is not grounded
        float yMag = 0;
        if (isGrounded) {
            jumpTimer = 0;
            // Add jump force to the player's velocity if the jump key is pressed
            if (jumpPressed) {
                jumpTimer++;
                jumpInitiated = true;
                yMag += JUMP_FORCE * 1.2f;
                isGrounded = false;
                Debug.Log($"jump, timer: {jumpTimer}");
            }
        }
        else { // If the player holds down jump, extends the amount of float time
            if (jumpHeld && jumpInitiated) {
                // Add additional height if the button is held
                if (jumpTimer % 30 == 0 && jumpTimer <= 90 && jumpTimer > 0) {
                    yMag += JUMP_FORCE / ((float) jumpTimer / 10f);
                    Debug.Log($"{jumpTimer}");
                }
                // Increment timer
                jumpTimer++;
            } else {
                jumpInitiated = false;
            }
        }
        // Set the player's new velocity
        rb.velocity = new Vector3(xMag, yMag + rb.velocity.y, 0);
        Debug.Log($"Velocity: {rb.velocity}, Speed: {xMag}, Grounded: {isGrounded}, Jump: {jumpPressed}, Jump Timer: {jumpTimer}");
    }
}
