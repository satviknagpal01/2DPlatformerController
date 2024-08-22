using UnityEngine;

[CreateAssetMenu]
public class ControllerStatsScriptable : ScriptableObject
{
    [Space]
    [Tooltip("Players layer mask to ignore in raycast")]
    public LayerMask playerLayer;
    [Tooltip("Configure input deadzone to deal with stick drift on controllers")]
    public float deadZone;

    [Space(2), Header("Movement")]
    [Tooltip("Max speed player can reach")]
    public float maxSpeed;
    [Tooltip("Values by which player will accelerate to max speed")]
    public float acceleration;
    [Tooltip("Values by which player will deaccelerate on ground")]
    public float groundDeceleration;
    [Tooltip("Values by which player will deaccelerate in air")]
    public float airDeceleration;

    [Space(2), Header("Jumps")]
    [Tooltip("Players jump velocity")]
    public float jumpPower;
    [Tooltip("Constante downward force applied while the player is grounded (helps with edges)")]
    public float groundingAcceleration;
    [Tooltip("Constante downward force applied while the player is in air (snappier jump)")]
    public float fallAcceleration;
    [Tooltip("Players max fall velocity")]
    public float maxFallSpeed;
    [Tooltip("Buffer time between consecutive jumps")]
    public float jumpBuffer;
    public float jumpEndEarlyMultiplier;
    [Tooltip("Time buffer where player can jump after leaving the edge")]
    public float coyoteTime;
    public int maxJumpCount;

    [Space(2), Header("Dash")]
    public float dashVelocity;
    public float dashBuffer;
}   
