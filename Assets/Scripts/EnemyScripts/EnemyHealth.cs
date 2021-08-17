using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : GeneralHealth
{
    public event System.Action OnDeath;
    MonoBehaviour camMono;
    private void Start()
    {
        camMono = Camera.main.GetComponent<MonoBehaviour>();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        CombatTextManager.Instance.SpawnText(new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z), damage, Color.red);
    }
    public override void GainHealth(float extraHealth)
    {
        base.GainHealth(extraHealth);
    }
    public override void Death()
    {
        OnDeath?.Invoke();
        camMono.StartCoroutine(SetEnemyActive(3));
        gameObject.SetActive(false);
    }
    private IEnumerator SetEnemyActive(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(true);
    }
}
