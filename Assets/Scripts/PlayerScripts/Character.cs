using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int damage = 0;
    private List<EnemyHealth> enemies = new List<EnemyHealth>();
    [HideInInspector] public bool isAllowedToAttack = true;
    [SerializeField] private float attackDelay = 0;
 
    private void OnTriggerStay2D(Collider2D collision)
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.Space) && isAllowedToAttack)
        {
            isAllowedToAttack = false;
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                damage = Random.Range(Experience.Instance.level, Experience.Instance.level * 2);
                //Debug.Log("You've hit " + enemies[i].name + " for " + damage + " damage ");
                enemies[i].TakeDamage(damage);
            }
            StartCoroutine(AttackDelayer(attackDelay));
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyHealth>() != null && !enemies.Contains(collision.GetComponent<EnemyHealth>()))
        {
            enemies.Add(collision.GetComponent<EnemyHealth>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyHealth>() != null)
        {
            enemies.Remove(collision.GetComponent<EnemyHealth>());
        }
    }
    private IEnumerator AttackDelayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAllowedToAttack = true;
    }  
}
