using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
public class JumpAction : PlayerAction
{

    public JumpAction(InputAction action, Player owner) : base(action, owner)
    {
        isLooping = false;
    }

    public override void Do()
    {
        if (owner.doJump)
        {
            //this constant (1.2) was discovered
            //by dividing the desired jump height by
            //the height actually reached.
            //this was the value I got regardless of the 
            //jump time. I also think that the timeToApex
            //probably affects this value, for this constant
            //the timeToApex was set to 0.01f.
            //I will look more into this at some other point.

            //I ACTUALLY GOT TO A HEIGHT OF 5 WHEN I MULTIPLIED 
            //BY THIS CONSTANT. 

            //I am genuinely impressed how accurate this formula
            //now is.


            //make the jump height 1/60 
            //then take the actual value reached by the jump
            //then do 1/60 / actual value
            //then do Desired height / actual value
            //and that gives you the most accurate
            //value to input as height for the jump.

            //OR set jump height to 1
            //and take the jump height reached by the jump
            //and do Desired Height / jump height reached for desired height of 1
            //and that gives you the properly scaled value.
            //timeToApex affects this so for a timeToApex
            //of 0.01 the jumpHeight scale modifier is 1.2f.

            //float modifier = 1.2f;//timeToApex / 0.00833333333f;
            float modifiedJumpHeight = owner.jumpHeight * 1.2f;

            //owner.animator.ResetTrigger("Jump");
            owner.jumping = true;
            owner.doJump = false;
            owner.ogJump = owner.transform.position;
            owner.jumpCount--;

            float gravity = 2 * modifiedJumpHeight / owner.timeToApex;
            float fallGravity = 2 * modifiedJumpHeight / owner.timeToFall;
            float projectedHeight = owner.timeToApex * gravity / 2f;

            float jumpForce = Mathf.Sqrt(2f * gravity * modifiedJumpHeight) * owner.rb.mass;
            float buttonTime = (jumpForce / (owner.rb.mass * gravity));

            Vector2 hold = owner.transform.up * jumpForce;

            owner.rb.velocity = new Vector2(owner.rb.velocity.x, 0f);
            owner.rb.AddForce(owner.transform.up * jumpForce, ForceMode2D.Impulse);

            owner.jumpTime = 0;
            owner.jumping = true;
            owner.jumpCanceled = true;
        }


        Vector2 localVel = owner.transform.InverseTransformDirection(owner.rb.velocity);

        if (localVel.y < 0 && owner.inAir) //If we are in the air and at the top of the arc then apply our fall speed to make falling more game-like
        {
            //animator.SetBool("falling", true);
            //we don't multiply by mass because forceMode2D.Force includes that in it's calculation.
            //set gravity to be fallGravity.
            float gravity = owner.fallGravity;
            Vector2 jumpVec = -owner.transform.up * (owner.fallMultiplier - 1)/* * 100f * Time.deltaTime*/;
            owner.rb.AddForce(jumpVec, ForceMode2D.Force);
        }
    }

}