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
    ///    A necessary function for any MovementOption object. Given <paramref name="x_move"/>,
    ///    <paramref name="y_move"/>, and the player's RigidBody component (<paramref name="rb"/>),
    ///    manipulates the player's velocity in the method best fit for the given scenario.
    /// </summary>
    /// <param name="x_move">Horizontal input from the player</param>
    /// <param name="y_move">Vertical input from the player</param>
    /// <param name="jump">Detects if the player has pressed the key assigned to jump</param>
    /// <param name="rb">The player's rigidbody component</param>
    public void Move(float x_move, float y_move, bool jump, Rigidbody rb);
}
