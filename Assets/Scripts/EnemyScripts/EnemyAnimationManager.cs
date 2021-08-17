using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    private RoamingEnemyBehaviour movement;
    private Animator anim;
    
    private void Start()
    {
        movement = GetComponent<RoamingEnemyBehaviour>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (movement.movementSpeed.magnitude != 0)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
        if (movement.movementSpeed.magnitude == 0 && movement.isAttacking)
        {
            anim.SetBool("IsAttacking", true);
        } 
        else
        {
            anim.SetBool("IsAttacking", false);
        }
    }
}
