using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///    The default movement option for 2D sections where acceleration and deceleration are
///    instantaneous.
/// </summary>
public class DefaultMovement2D : IMovementOption
{
    /// <summary>The rate at which the player's velocity increases</summary>
    const float ACCELERATION = 400f;
    /// <summary>The player's maximum speed</summary>
    const float MAX_SPEED = 15f;
    /// <summary>The vertical acceleration due to the player's personal gravity</summary>
    const float GRAVITY = -9.8f;
    /// <summary>The amount of force exerted when the player is shot into the air via the jump command</summary>
    const float JUMP_FORCE = 30f;
    /// <summary>The amount of time the player can hold JUMP for to continue adding force</summary>
    int jump_timer = 0;

    public void Move(float x_move, float y_move, bool is_grounded, bool jump, Rigidbody rb) {
        // Get x-axis input and move character horizontally (acceleration and deceleration are instant)
        float x_mag =  Mathf.Clamp(x_move * ACCELERATION * Time.fixedDeltaTime, -MAX_SPEED, MAX_SPEED);
        // Apply gravity if the player is not grounded
        float y_mag = is_grounded ?
            0 : Mathf.Clamp(rb.velocity.y + (GRAVITY * Time.fixedDeltaTime), GRAVITY * 3, JUMP_FORCE * 2);
        // Add jump force to the player's velocity if the jump key is pressed
        if (jump) {
            y_mag += JUMP_FORCE;
        }
        
        // Set the player's new velocity
        rb.velocity = new Vector3(x_mag, y_mag, 0);
        Debug.Log($"Velocity: {rb.velocity}, Speed: {x_mag}, Grounded: {is_grounded}, Jump: {jump}");
    }
}
