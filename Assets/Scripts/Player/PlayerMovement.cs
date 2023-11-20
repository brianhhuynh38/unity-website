using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///    Script that controls the player's movement. The player may be able to change movement 
///    options as they progress through the gallery, which can be done within this script.
///    Movement is controlled by the current <see cref="IMovementOption"/>.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>The player's RigidBody component</summary>
    Rigidbody rb;
    /// <summary>The player's current <see cref="IMovementOption"/>. The default option is <see cref="DefaultMovement"/> </summary>
    IMovementOption move_option;
    /// <summary>Whether the player is grounded or not</summary>
    bool is_grounded = false;

    float x_move;
    float y_move;
    bool jump_pressed;
    bool jump_held;

    // Start is called once upon instantiation
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        move_option = new DefaultMovement2D();
    }

    // Update is called at the beginning of every frame
    void Update() {
        // Get player inputs
        x_move = Input.GetAxisRaw("Horizontal");
        y_move = Input.GetAxisRaw("Vertical");
        jump_pressed = Input.GetButtonDown("Jump");
        jump_held = Input.GetButton("Jump");
    }

    void LateUpdate() {
        // Use the currently assigned movement option
        move_option.Move(x_move, y_move, is_grounded, jump_pressed, jump_held, rb);
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
