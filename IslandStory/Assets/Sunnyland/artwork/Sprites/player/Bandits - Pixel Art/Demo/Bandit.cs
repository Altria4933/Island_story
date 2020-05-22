﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bandit : MonoBehaviour {

    [SerializeField] float      m_speed = 5.0f;
    [SerializeField] float      m_jumpForce = 6.0f;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;
    
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public int Cherry = 0;
    public Text CherryNum;


    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
	
	// Update is called once per frame
	void Update () {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if(m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if (inputX < 0)
            GetComponent<SpriteRenderer>().flipX = false;

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("e")) {
            if(!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }
            
        //Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        //Attack
        else if(Input.GetKeyDown("z")) {
            m_animator.SetTrigger("Attack");
        }
        /*
        else if(Input.GetMouseButtonDown(0)) {
            m_animator.SetTrigger("Attack");
        }
        */

        //Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded) {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

         else if (Input.GetKeyDown("space") && m_grounded) {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }
         

        /* else if (Input.GetKeyDown("space") && coll.IsTouchingLayers(ground))
         {
            m_animator.SetBool("Jump", true);
         }
         */
        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);
        

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    //Collect Cherry
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
         Destroy(collision.gameObject);
         Cherry += 1;
         CherryNum.text = Cherry.ToString();
        }
    }

    //Eliminate Enemies
    private void onCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemies")
        {
            Destroy(collision.gameObject);
        }
        
    }
}
