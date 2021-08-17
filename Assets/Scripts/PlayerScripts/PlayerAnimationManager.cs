using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private TopDownMovementScript movement;
    private Animator anim;
    public LayerMask layerMask;
    public DropDisplayer window;
    private void Start()
    {
        movement = GetComponent<TopDownMovementScript>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (movement.IsMoving() && movement.speed != 0)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
        if (!movement.IsMoving() && Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("IsAttacking", true);
        } else
        {
            anim.SetBool("IsAttacking", false);
        }
        var collider = Physics2D.OverlapCircle(transform.position, 4, layerMask);
        if (collider == null)
        {
            window.SetWindowInactive();
        }
    }
}
