using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Determines the logic of DivineProtection skill prefab.
/// </summary>
public class DivineProtectionLogic : MonoBehaviour
{
    public BuffSkillObject creator;
    public void Start()
    {
        Expire(creator.duration);
    }
    /// <summary>
    /// Slows down all mobs within a certain radius around the player.
    /// </summary>
    public void Update()
    {
        foreach (var mob in FindObjectsOfType<Enemy>())
        {
            if (Vector3.Distance(mob.transform.position, FindObjectOfType<PlayerHealth>().transform.position) < 4)
            {
                mob.speed = 3 - 0.2f * creator.level;
            } 
            else
            {
                mob.speed = 3;
            }
        }
    }
    /// <summary>
    /// Public coroutine wrapper.
    /// </summary>
    public void Expire(float _duration)
    {
        StartCoroutine(IExpire(_duration));
    }
    /// <summary>
    /// Coroutine destroying instantiated prefab from the scene.
    /// </summary>
    private IEnumerator IExpire(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        foreach (var mob in FindObjectsOfType<Enemy>())
        {
            mob.speed = 3;
        }
    }
}
