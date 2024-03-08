using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
public class MoveAction : PlayerAction {

    public MoveAction(InputAction action, Player owner) : base(action, owner) {
        isLooping = false;
    }

    public override void Do() {
        Transform camTransform = Camera.main.transform;
        Vector2 moveInput = action.ReadValue<Vector2>();
        Vector2 moveDirection = ((moveInput.normalized.x * camTransform.right.normalized) + (moveInput.normalized.y * camTransform.up.normalized)) * owner.moveSpeed;
    
        if (Mathf.Abs(moveInput.x) > 0.01 && (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y)))
        {
            if (moveInput.x > 0)
            {
               owner.xAxis = moveInput.x <= 0.75f ? 0.5f : 1f;
            }
            else if (moveInput.x < 0)
            {
                owner.xAxis = moveInput.x <= -0.75f ? -1 : -0.5f;
            }
        }


        if (Mathf.Abs(moveInput.y) > 0.01)
        {
            if (moveInput.y > 0)
            {
                owner.yAxis = moveInput.y <= 0.75f ? 0.5f : 1f;
            }
            else if (moveInput.y < 0)
            {
                owner.yAxis = moveInput.y <= -0.75f ? -1 : -0.5f;
            }
        }
        if (moveInput.x != 0 && moveInput.y != 0)
            owner.HandleInstantaneousRotation();
    }
}