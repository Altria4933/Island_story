using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : MonoBehaviour
{
    private Rigidbody2D rb;
     private Animator Anim;
    private Collider2D coll;
    public Transform top, bottom;
    public float Speed;
    private float TopY, BottomY;
    public int maxHealth = 100;
    int currentHealth;

    private bool isUp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        TopY = top.position.y;
        BottomY = bottom.position.y;
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);
            if (transform.position.y > TopY)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -Speed);
            if (transform.position.y < BottomY)
            {
                isUp = true;
            }
        }
    }


    public void takeDmg (int dmg)
    {
          currentHealth -= dmg;
        Debug.Log("Eagle took" + dmg + "dmg, " + currentHealth + "HP left.");
        //play hurt anim

        if(currentHealth <= 0)
        {
            Death();
        }
    }

   void Death()
    {
        Debug.Log("frog dead");
        Anim.SetTrigger("death");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }
}
