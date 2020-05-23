using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bandit : MonoBehaviour
{

    [SerializeField] float m_speed = 5.0f;
    [SerializeField] float m_jumpForce = 6.0f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    private bool                m_isDead = false;
    

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public Transform attackPoint;
    public float attackRange = 0.4f;
    public LayerMask enemylayers;
    public int atkdmg = 20;
    float atkRate = 2f;
    float nextAttackTime = 0f;
    //public attack m_attack;


    public int Cherry = 0;
    public Text CherryNum;

    public int Gem = 0;
    public Text GemNum;


    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1f;
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
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
        if (currentHealth <= 0)
        {
            if (m_isDead == false)
            {
                m_animator.SetTrigger("Death");
                m_isDead = true;
                Debug.Log("You dead");
                Time.timeScale = 0f;
                FindObjectOfType<MenuBehaviour>().endGame();
            }
        }
        //keep the souce code for debug 
        /*if (Input.GetKeyDown("e")) {
            if(!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }  */

        //Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        //Attack
        else if (Input.GetKeyDown("z"))
        {
            //Attack timer so the player wont go crazy
            if (Time.time >= nextAttackTime)
            {
                attack();
                nextAttackTime = Time.time + 1f / atkRate;

            }
            //m_attack.Update();

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
        else if (Input.GetKeyDown("space") && m_grounded)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        else if (Input.GetKeyDown("space") && m_grounded)
        {
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

    void attack()
    {
        m_animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemylayers);

        //damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy_Frog>().takeDmg(atkdmg);
            Debug.Log("you hit " + enemy.name);
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        m_animator.SetTrigger("Hurt");
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
        else if (collision.tag == "Gem")
        {
         Destroy(collision.gameObject);
         Gem += 1;
         GemNum.text = Gem.ToString();
        }
    }
    
    


    //Eliminate Enemies
    private void onCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            Destroy(collision.gameObject);
        }

    }

    



}


