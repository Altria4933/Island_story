using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy_Goblin : Monster
{

    public Transform leftpoint, rightpoint;
    private float leftx, rightx;
    public float Speed;
    private bool Faceleft = true;
    float timeLeft = 5.0f;





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
        is_dead = false;
        this.Monster_name = "goblin";
        


    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
            attack();
            timeLeft = 5.0f;
            }
    }
    void Movement()
    {
        if (Faceleft)
        {
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }


    void attack()
    {
        Debug.Log(Monster_name + "Attacks. (testing)");
      
    }
    

}
