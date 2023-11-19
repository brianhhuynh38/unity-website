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
    /// <summary>Whether the player is groudned or not</summary>
    bool is_grounded;

    // Start is called once upon instantiation
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        move_option = new DefaultMovement2D();
        is_grounded = false;
    }

    // FixedUpdate is called at a rate specified by the editor
    void FixedUpdate()
    {
        float x_move = Input.GetAxisRaw("Horizontal");
        float y_move = Input.GetAxisRaw("Vertical");
        bool jump = Input.GetButtonDown("Jump");
        move_option.Move(x_move, y_move, is_grounded, jump, rb);
    }

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

    void OnCollisionExit(Collision other) {
        is_grounded = false;
    }
}
