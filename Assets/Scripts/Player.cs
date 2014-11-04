using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Player :AbsPlayer
{
    public override void Start()
    {
        this.animator = this.gameObject.GetComponent<Animator>();
        this.impulse = 0.2f;
        this.velocityX = 0.2f;
        this.velocityY = 0.2f; 
    }

    public override void Update()
    {

        //walk
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            this.transform.eulerAngles = new Vector2(0, 0);
            this.Walk();

        }
        else
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                this.transform.eulerAngles = new Vector2(0, 180);
                this.Walk();
            }
            else
                this.animator.SetBool("Running", false);

        //Jump
        bool inputJ = Input.GetButtonDown("Jump");
        var pos = this.transform.position;
        if (inputJ && !this.jumping)
        {
            this.animator.SetBool("Jumping", true);
            this.jumping = true;
            this.velocityY = impulse;
            this.yInitial = pos.y;
        }

        if (this.jumping)
        {
            pos.y += this.velocityY;
            this.velocityY -= this.gravity;
            if (pos.y <= this.yInitial)
            {
                this.jumping = false;
                this.animator.SetBool("Jumping", false);
                pos.y = this.yInitial;
            }
        }
        this.transform.position = pos;


        //Attack2
        
        if (Input.GetButtonDown("Fire1"))
        {
            this.Attack2();
        }
        else
        {
            animator.SetBool("Attacking", false);
        }

        //block

        if (Input.GetButtonDown("Fire3"))
        {
            this.Block();
        }
        else
        {
            animator.SetBool("Defending", false);
        }

    }
}

