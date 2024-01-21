using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///    An interface inherited by any movement options meant for player control. Each option for movement 
///    can differ from being 2D or 3D and modifies the player's RigidBody component. This should also 
///    control the animations for the character in each given scenario.
/// </summary>
public interface IMovementOption
{
    /// <summary>
    ///    A necessary function for any MovementOption object. Given <paramref name="xMove"/>,
    ///    <paramref name="yMove"/>, and the player's RigidBody component (<paramref name="rb"/>),
    ///    manipulates the player's velocity in the method best fit for the given scenario.
    /// </summary>
    /// <param name="xMove">Horizontal input from the player</param>
    /// <param name="yMove">Vertical input from the player</param>
    /// <param name="isGrounded">If the player is touching a floor</param>
    /// <param name="jumpPressed">Detects if the player has pressed the key assigned to jump</param>
    /// <param name="jumpHeld">Detects if the player has held down the key assigned to jump</param>
    /// <param name="rb">The player's rigidbody component</param>
    public void Move(float xMove, float yMove, bool isGrounded, bool jumpPressed, bool jumpHeld, Rigidbody rb);
}
