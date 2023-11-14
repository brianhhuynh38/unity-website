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
    const float MAX_SPEED = 10f;
    /// <summary>The vertical acceleration due to the player's personal gravity</summary>
    const float GRAVITY = -300f;
    /// <summary>The amount of force exerted when the player is shot into the air via the jump command</summary>
    const float JUMP_FORCE = 70f;

    bool is_grounded = true;

    public void Move(float x_move, float y_move, bool jump, Rigidbody rb) {
        // Get x-axis input and move character horizontally (acceleration and deceleration are instant)
        float x_vector = x_move != 0 ?
            Mathf.Clamp(x_move * ACCELERATION * Time.fixedDeltaTime, -MAX_SPEED, MAX_SPEED) : 0;
        // TODO: Finish the vertical gravity movement
        float y_vector = is_grounded ?
            0 : GRAVITY * Time.fixedDeltaTime;
        // TODO: Make jump controls with collisions
        rb.velocity = new Vector3(x_vector, y_vector, 0);
        Debug.Log($"Velocity: {rb.velocity}, Speed: {x_vector}");
    }
}
