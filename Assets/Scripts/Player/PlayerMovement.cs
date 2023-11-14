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

    // Start is called once upon instantiation
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        move_option = new DefaultMovement2D();
    }

    // FixedUpdate is called at a rate specified by the editor
    void FixedUpdate()
    {
        float x_move = Input.GetAxisRaw("Horizontal");
        float y_move = Input.GetAxisRaw("Vertical");
        bool jump = Input.GetButtonDown("Jump");
        move_option.Move(x_move, y_move, jump, rb);
    }
}
