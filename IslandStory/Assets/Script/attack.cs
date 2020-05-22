using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{

    public Animator animator;
    
    // Update is called once per frame
    public void Update()
    {
        animator = GetComponent<Animator>();
        Prefomattack();
    }

    public void Prefomattack()
    {
       animator.SetTrigger("Attack");
    }
}
