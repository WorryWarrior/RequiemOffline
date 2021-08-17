using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingEnemyBehaviour : MonoBehaviour
{  
    [HideInInspector] public Rigidbody2D rb;
    private Enemy enemy;

    [HideInInspector] public bool isAllowedToAttack = true;
    [SerializeField] private float attackDelay = 2;
    private Transform target;
    [HideInInspector] public bool isAllowedToChase = true;

    public float chasingDistance = 5f;
    [HideInInspector] public float initialChaisngDistance = 0;

    [HideInInspector] public Vector2 movementSpeed;
    Vector3 faceDirection;

    PlayerHealth targetHealth;
    [HideInInspector] public bool isAttacking = false;
    public int damageReducingConstant;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        targetHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        target = targetHealth.transform;
        initialChaisngDistance = chasingDistance;
    }

    private void Update()
    {
        if (enemy.health.Health != enemy.health.MaxHealth)
        {
            chasingDistance = 30f;
        }
        if (isAttacking)
        {
            Attack(targetHealth);
        }
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        if (target != null && Vector2.Distance(transform.position, target.position) < chasingDistance)
        {
            if (isAllowedToChase)
            {
                enemy.Chase(target);
            }
        }
        else
        {
            enemy.Roam();
        }
        movementSpeed = currentPos - rb.position;
        FaceDirection();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            isAllowedToChase = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAllowedToChase = true;
            isAttacking = false;
        }
    }

    private void Attack(PlayerHealth target)
    {
        enemy.damage = Experience.Instance.level;/*Random.Range(Experience.Instance.level, Mathf.FloorToInt(1.4f * Experience.Instance.level + 1));*/
        if (isAllowedToAttack)
        {
            var modifiedDamage = enemy.damage - damageReducingConstant;
            if (modifiedDamage > 0)
                target.TakeDamage(modifiedDamage);
            else
                target.TakeDamage(0);
            isAllowedToAttack = false;
            StartCoroutine(AttackDelayer(attackDelay));
        }
    }

    private void FaceDirection()
    {
        rb.transform.eulerAngles = faceDirection;
        if (movementSpeed.x > 0)
        {
            faceDirection.Set(0, 180, 0);
        }
        if (movementSpeed.x < 0)
        {
            faceDirection.Set(0, 0, 0);
        }
    }
   
    private IEnumerator AttackDelayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAllowedToAttack = true;
    }
}
