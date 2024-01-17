using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///    Script that controls the player's movement. The player may be able to change movement 
///    options as they progress through the gallery, which can be done within this script.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>The player's RigidBody component</summary>
    Rigidbody rb;

    /// <summary>The player's sprite</summary>
    Sprite sprite;
    /// <summary>The max height the player's sprite bounces along</summary>
    [SerializeField] float sprite_bounce_height;
    /// <summary>An integer that counts upwards to advance through animation frames</summary>
    int anim_counter;

    /// <summary>Whether the player is grounded or not</summary>
    bool is_grounded = false;
    /// <summary>Horizontal player input</summary>
    float x_move;
    /// <summary>Vertical player input</summary>
    float y_move;
    /// <summary>Whether the jump button is pressed</summary>
    bool jump_pressed;
    /// <summary>Whether the jump button is held down</summary>
    bool jump_held;
    /// <summary>How long the jump button is held down for</summary>
    int jump_timer = 0;
    /// <summary>Whether the jump was initiated</summary>
    bool jump_initiated = false;
    /// <summary>The rate at which the player's velocity increases</summary>
    [SerializeField] float acceleration = 20f;
    /// <summary>The player's maximum speed</summary>
    [SerializeField] float speed = 10f;
    /// <summary>The amount of force exerted when the player is shot into the air via the jump command</summary>
    [SerializeField] float jump_force = 7f;

    // Start is called once upon instantiation
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        sprite = GameObject.Find("Player/Sprite").GetComponent<Sprite>();
    }

    // Update is called at the beginning of every frame
    void Update() {
        // Get player inputs
        x_move = Input.GetAxisRaw("Horizontal");
        y_move = Input.GetAxisRaw("Vertical");
        jump_pressed = Input.GetButtonDown("Jump");
        jump_held = Input.GetButton("Jump");
    }

    // LateUpdate is called after all other Updates are called
    void LateUpdate() {
        // Get x-axis input and move character horizontally
        float x_mag =  Mathf.Clamp(
            (x_move * acceleration * Time.fixedDeltaTime) + rb.velocity.x,
            -speed,
            speed
            );
        // Apply gravity if the player is not grounded
        float y_mag = 0;
        if (is_grounded) {
            jump_timer = 0;
            // Add jump force to the player's velocity if the jump key is pressed
            if (jump_pressed) {
                jump_timer++;
                jump_initiated = true;
                y_mag += jump_force * 1.2f;
                is_grounded = false;
                Debug.Log($"jump, timer: {jump_timer}");
            }
        }
        else { // If the player holds down jump, extends the amount of float time
            if (jump_held && jump_initiated) {
                // Add additional height if the button is held
                if (jump_timer % 30 == 0 && jump_timer <= 90 && jump_timer > 0) {
                    y_mag += jump_force / ((float) jump_timer / 10f);
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

    /// <summary>
    ///    Runs whenever the player comes into contact with another collider
    /// </summary>
    /// <param name="other">The reference to the collision with info on the other object</param>
    void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag) {
            case "Floor":
                is_grounded = true;
                break;
            default:
                is_grounded = false;
                break;
        }
    }

    /// <summary>
    ///    Runs whenever the player leaves contact with another collider
    /// </summary>
    /// <param name="other">The reference to the collision with info on the other object</param>
    void OnCollisionExit(Collision other) {
        is_grounded = false;
    }
}
