using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class DashAction : PlayerAction
{
    float timeTaken;
    int frames = 0;
    float currentTime = 0f;
    Vector2 startPos;

    public DashAction(Player owner) : base(owner)
    {
        isLooping = true;
        cooldown = 2;
    }

    public override void Do() {
        Debug.Log("Dashing");
        if (owner.isHitStunned) {
            StopDash();
            return;
        }
        
        bool end = false;

        if (owner.shouldDash == true) {
            Reset();
        }

        float timeToDash = owner.dashFrames / 60f;
        float modDashDist = owner.dashDist * owner.dashModifier;

        float acceleration = 2f * modDashDist / Mathf.Pow(timeToDash, 2f);
        float initVel = Mathf.Sqrt(2f * acceleration * modDashDist);
        float curVel = initVel;
        float dashForce = initVel * owner.rb.mass;

        if (frames < (owner.dashFrames - 6) && owner.curDirection == owner.dashDirection) {
            end = true;
        } else {
            curVel = initVel - acceleration * (timeToDash - currentTime);
        }
        
        owner.rb.velocity = getFromDirection(owner.dashDirection) * curVel;

        frames--;
        currentTime -= Time.deltaTime;
        if (currentTime < 0.0001)
        {
            currentTime = 0;
        }
        Debug.Log(frames+" "+end);
        Debug.Log(currentTime);

        if (frames <= 0 || end) {
            StopDash();
        }
    }
    
    private void Reset() {
        startPos = owner.rb.position;
        owner.shouldDash = false;
        frames = owner.dashFrames;
        timeTaken = System.DateTime.Now.Millisecond;
        float timeToDash = frames / 60f;
        currentTime = timeToDash;
        owner.curDirection = owner.dashDirection;
        owner.spriteParent.transform.rotation = Quaternion.Euler(0, owner.dashDirection == Direction.Left ? 180 : 0, 0);
        owner.state = PlayerState.dashing;
        owner.launchParticles.Play();
    }
    private void StopDash() {
        owner.state = PlayerState.None;
        owner.dashDirection = Direction.None;
        owner.launchParticles.Stop();
        Stop();
    }
    
    private Vector2 getFromDirection(Direction d) {
        return d == Direction.Left ? Vector2.left : d == Direction.Right ? Vector2.right : Vector2.zero; 
    }
}
