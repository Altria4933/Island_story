﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator Anim;
    private Collider2D Coll;
    public LayerMask Ground;
    public Transform leftpoint, rightpoint;
    public float Speed,JumpForce;
    private float leftx, rightx;
    public int maxHealth = 100;
    int currentHealth;
    private Bandit bandit;

    private bool Faceleft = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();
        bandit = GameObject.FindGameObjectWithTag("Player").GetComponent<Bandit>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        SwitchAnim();
    }

    void Movement()
    {
        if(Faceleft)
        {
            if(Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-Speed, JumpForce);
            }
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1,1,1);
                Faceleft = false;
            }
        }
        else
        {
            if(Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(Speed, JumpForce);
            }
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1,1,1);
                Faceleft = true;
            }
        }


    }

    public void takeDmg(int dmg)
    {
        currentHealth -= dmg;
        Debug.Log("Frog took" + dmg + "dmg, " + currentHealth + "HP left.");
        //play hurt anim

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    void SwitchAnim()
    {
        if (Anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }
        if (Coll.IsTouchingLayers(Ground) && Anim.GetBool("falling"))
        {
            Anim.SetBool("falling", false);
        }
    }

    void Death()
    {      
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Anim.SetTrigger("death");
        Debug.Log("frog dead");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            bandit.TakeDamage(20);
            
            Debug.Log("you hut 20");

        }
    }


}
