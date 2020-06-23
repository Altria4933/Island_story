using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airwall : MonoBehaviour
{
    public GameObject Airwall;
    public GameObject Boss;
    public bool boss_isDead ;
    public Enemy_Goblin gob;


    void Start ()
    {
        
        boss_isDead = gob.is_dead;
    }

    void Update()
    {
       
        boss_isDead = gob.is_dead;
        Debug.Log( Boss.activeSelf+" " + gob.getDeath() + " " +gob.is_dead + " " + boss_isDead);
    }
    void bossDead()
    {
       
       if (!boss_isDead)
        {
            Airwall.SetActive(false);
        }

    }
}


