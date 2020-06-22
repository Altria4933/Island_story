using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator Anim;
    public Collider2D Coll;
    public LayerMask Ground;
    public int maxHealth = 100;
    public int currentHealth;
    public Bandit bandit;
    public bool is_dead = false;
    public string Monster_name;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {

        SwitchAnim();
    }

    void Movement()
    {
       
    }

    public void takeDmg(int dmg)
    {
        currentHealth -= dmg;
        Debug.Log(Monster_name + " took" + dmg + "dmg, " + currentHealth + "HP left. (testing)");
        //play hurt anim

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void SwitchAnim()
    {
        
    }

    void Death()
    {
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Anim.SetTrigger("death");
        Debug.Log(name +" dead");
        bandit.addGem();
        is_dead = true;


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
