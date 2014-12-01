using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class AbsCharacter : MonoBehaviour
{
    public float maxSpeed;
    public float maxImpulse;
    protected float impulse;
    protected float velocityX;
    protected float velocityY;
    public float gravity;
    protected int axisX;
    protected int axisY;
    protected float yInitial;
    protected int hp;
    protected Animator animator;
    public Vector2 speed;


    public abstract void Walk();
    public abstract void Fall();
    public abstract void Hit();
    public abstract void Die();
    public abstract void Attack();
    public abstract void Attack2();
    public abstract void Block();

}

