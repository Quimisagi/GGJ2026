using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player side-to-side movement across three lanes.
/// Lane 1: Left, Lane 2: Center (Start), Lane 3: Right.
/// </summary>
public class PlayerMove : MonoBehaviour
{
    private Vector3 pos;
    
    [Header("Movement Settings")]
    public float MaxPos = 5.0f;
    public float LaneDistance = 5.0f;

    // Public property to track the current lane (1, 2, or 3)
    // Initialized to 2 as the starting lane
    public int CurrentLane { get; private set; } = 2;

    void Start()
    {
        // Initialize position to the starting transform position
        pos = transform.position;
    }

    void Update()
    {
        PlayerSideMove();
    }

    /// <summary>
    /// Processes input and updates the player's X position based on lane logic.
    /// </summary>
    void PlayerSideMove()
    {
        // Move Left: Only if we are in lane 2 or 3
        if (Input.GetKeyDown(KeyCode.A) && CurrentLane > 1)
        {
            pos.x -= LaneDistance;
            CurrentLane--;
        }
        // Move Right: Only if we are in lane 1 or 2
        else if (Input.GetKeyDown(KeyCode.D) && CurrentLane < 3)
        {
            pos.x += LaneDistance;
            CurrentLane++;
        }

        // Keep the original clamping logic as requested, 
        // ensuring x stays within defined bounds
        pos.x = Mathf.Clamp(pos.x, -MaxPos, MaxPos);

        // Apply the updated position to the transform
        transform.position = pos;
    }

    /// <summary>
    /// Returns the current internally tracked position vector.
    /// </summary>
    public Vector3 GetPlayerPos()
    {
        return pos;
    }
}
