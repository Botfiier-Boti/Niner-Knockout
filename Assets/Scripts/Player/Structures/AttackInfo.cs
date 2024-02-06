using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

//used for storing the data of attacks in 
//the player's attack data dictionary.
[Serializable]
public struct AttackInfo
{
    public AttackInfo(float launchAngle, float attackDamage, float baseKnockback, float knockbackScale, int hitLag)
    {
        this.launchAngle = launchAngle;
        this.attackDamage = attackDamage;
        this.baseKnockback = baseKnockback;
        this.knockbackScale = knockbackScale;
        this.hitLag = hitLag;
    }

    /// <summary>
    /// The percentage of damage added to the player's damage meter upon a successful hit.
    /// </summary>
    public float attackDamage;

    /// <summary>
    /// The amount of additional hitlag 
    /// Applied by this attack when 
    /// launching.
    /// </summary>
    public int hitLag;

    /// <summary>
    /// The direction the enemy is sent in if this attack lands. In Degrees.
    /// </summary>
    public float launchAngle;

    /// <summary>
    /// The base knockback of this attack, regardless of the player's percentage.
    /// </summary>
    public float baseKnockback;

    /// <summary>
    /// Describes how much knockback and percent scale.
    /// </summary>
    public float knockbackScale;


}
