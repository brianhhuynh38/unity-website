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
    [SerializeField] float spriteBounceHeight;
    /// <summary>An integer that counts upwards to advance through animation frames</summary>
    int animCounter;

    /// <summary>Whether the player is grounded or not</summary>
    bool isGrounded = false;
    /// <summary>Horizontal player input</summary>
    float xMove;
    /// <summary>Vertical player input</summary>
    float yMove;
    /// <summary>Whether the jump button is pressed</summary>
    bool jumpPressed;
    /// <summary>Whether the jump button is held down</summary>
    bool jumpHeld;
    /// <summary>How long the jump button is held down for</summary>
    int jumpTimer = 0;
    /// <summary>Whether the jump was initiated</summary>
    bool jumpInitiated = false;
    /// <summary>The rate at which the player's velocity increases</summary>
    [SerializeField] float acceleration = 20f;
    /// <summary>The player's maximum speed</summary>
    [SerializeField] float speed = 10f;
    /// <summary>The amount of force exerted when the player is shot into the air via the jump command</summary>
    [SerializeField] float jumpForce = 7f;

    // Start is called once upon instantiation
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        sprite = GameObject.Find("Player/Sprite").GetComponent<Sprite>();
    }

    // Update is called at the beginning of every frame
    void Update() {
        // Get player inputs
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");
        jumpPressed = Input.GetButtonDown("Jump");
        jumpHeld = Input.GetButton("Jump");
    }

    // LateUpdate is called after all other Updates are called
    void LateUpdate() {
        // Get x-axis input and move character horizontally
        float xMag =  Mathf.Clamp(
            (xMove * acceleration * Time.fixedDeltaTime) + rb.velocity.x,
            -speed,
            speed
            );
        // Apply gravity if the player is not grounded
        float yMag = 0;
        if (isGrounded) {
            jumpTimer = 0;
            // Add jump force to the player's velocity if the jump key is pressed
            if (jumpPressed) {
                jumpTimer++;
                jumpInitiated = true;
                yMag += jumpForce * 1.2f;
                isGrounded = false;
                Debug.Log($"jump, timer: {jumpTimer}");
            }
        }
        else { // If the player holds down jump, extends the amount of float time
            if (jumpHeld && jumpInitiated) {
                // Add additional height if the button is held
                if (jumpTimer % 30 == 0 && jumpTimer <= 90 && jumpTimer > 0) {
                    yMag += jumpForce / ((float) jumpTimer / 10f);
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

    /// <summary>
    ///    Runs whenever the player comes into contact with another collider
    /// </summary>
    /// <param name="other">The reference to the collision with info on the other object</param>
    void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag) {
            case "Floor":
                isGrounded = true;
                break;
            default:
                isGrounded = false;
                break;
        }
    }

    /// <summary>
    ///    Runs whenever the player leaves contact with another collider
    /// </summary>
    /// <param name="other">The reference to the collision with info on the other object</param>
    void OnCollisionExit(Collision other) {
        isGrounded = false;
    }
}
