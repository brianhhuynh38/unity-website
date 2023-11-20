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
    const float ACCELERATION = 15f;
    /// <summary>The player's maximum speed</summary>
    const float MAX_SPEED = 10f;
    /// <summary>The amount of force exerted when the player is shot into the air via the jump command</summary>
    const float JUMP_FORCE = 7f;
    /// <summary>The amount of time the player can hold JUMP for to continue adding force</summary>
    int jump_timer = 0;
    /// <summary>If the player has started a jump</summary>
    bool jump_initiated = false;

    public void Move(float x_move, float y_move, bool is_grounded, bool jump_pressed, bool jump_held, Rigidbody rb) {
        // Get x-axis input and move character horizontally
        float x_mag =  Mathf.Clamp(
            (x_move * ACCELERATION * Time.fixedDeltaTime) + rb.velocity.x,
            -MAX_SPEED,
            MAX_SPEED
            );
        // Apply gravity if the player is not grounded
        float y_mag = 0;
        if (is_grounded) {
            jump_timer = 0;
            // Add jump force to the player's velocity if the jump key is pressed
            if (jump_pressed) {
                jump_timer++;
                jump_initiated = true;
                y_mag += JUMP_FORCE * 1.2f;
                is_grounded = false;
                Debug.Log($"jump, timer: {jump_timer}");
            }
        }
        else { // If the player holds down jump, extends the amount of float time
            if (jump_held && jump_initiated) {
                // Add additional height if the button is held
                if (jump_timer % 30 == 0 && jump_timer <= 90 && jump_timer > 0) {
                    y_mag += JUMP_FORCE / ((float) jump_timer / 10f);
                    Debug.Log($"{jump_timer}");
                }
                // Increment timer
                jump_timer++;
            } else {
                jump_initiated = false;
            }
        }
        // Set the player's new velocity
        rb.velocity = new Vector3(x_mag, y_mag + rb.velocity.y, 0);
        Debug.Log($"Velocity: {rb.velocity}, Speed: {x_mag}, Grounded: {is_grounded}, Jump: {jump_pressed}, Jump Timer: {jump_timer}");
    }
}
