using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
public class PlayerAction {
    private const long TICKS_IN_MILLISECOND = 10000;

    public Player owner;

    public InputAction action;

    public bool isLooping = false;

    public bool running {get; private set;} = false;

    public long cooldown;

    public long lockoutTime = 0;

    private long nextUse;
    
    private long whenFree;

    public PlayerAction(InputAction action, long cooldown, Player owner) {
        this.action = action;
        this.cooldown = cooldown;
        this.owner = owner;
    }

    public PlayerAction(long cooldown, Player owner) {
        this.action = null;
        this.cooldown = cooldown;
        this.owner = owner;
    }

    public PlayerAction(InputAction action, Player owner) {
        this.action = action;
        this.cooldown = 0;
        this.owner = owner;
    }

     public PlayerAction(Player owner) {
        this.action = null;
        this.cooldown = 0;
        this.owner = owner;
    }

    public void Run() {
        if (isOnCooldown()) {
            return;
        }
        running = true;
        Do();
        End();
    }

    public virtual void Do() {
    }

    private void End() {
        if (isLooping == true && running == true)
            return;
        nextUse = System.DateTime.Now.Ticks + cooldown * TICKS_IN_MILLISECOND;
        whenFree = System.DateTime.Now.Ticks + lockoutTime * TICKS_IN_MILLISECOND;
        running = false;
    }

    public void Stop() {
        running = false;
        End();
    }

    public bool isOnCooldown() {
        return nextUse > System.DateTime.Now.Ticks;
    }

    public bool isHogging() {
        return whenFree > System.DateTime.Now.Ticks;
    }
}