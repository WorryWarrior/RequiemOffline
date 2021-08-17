using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Determines the behaviour of fireball-like abilities.
/// </summary>
public class FireballPhysics : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [HideInInspector] public float damage;
    public float multiplier;
    public ActiveSkillObject skill;
    private Rigidbody2D rb;
    private GameObject fireballTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// Sets up the damage and acquires the target.
    /// </summary>
    private void Start()
    {
        damage = multiplier * skill.level;
        fireballTarget = EnemyTargetManager.Instance.target;
    }
    /// <summary>
    /// Moves the prefab towards the target and handles its rotation.
    /// </summary>
    private void FixedUpdate()
    {
        if (EnemyTargetManager.Instance.target != null)
        {
            if (fireballTarget == EnemyTargetManager.Instance.target)
            {
                RotationHandler(EnemyTargetManager.Instance.target.transform);
            }
            rb.position = Vector2.MoveTowards(rb.position, fireballTarget.transform.position, speed * Time.fixedDeltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Destroys the prefab upon collision, deals damage if collided
    /// object is enemy.
    /// </summary>
    /// <param name="collision">Collision object.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
    /// <summary>
    /// Creates a fancy rotation of a projectile.
    /// </summary>
    /// <param name="target">Prefab component.</param>
    private void RotationHandler(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
