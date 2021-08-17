using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGiver : MonoBehaviour
{
    private EnemyHealth health;
    [SerializeField] private int experienceGiven = 0;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
    }
    private void OnEnable()
    {
        health.OnDeath += GetExperience;
    }

    private void OnDisable()
    {
        health.OnDeath -= GetExperience;
    }

    private void GetExperience()
    {
        Experience.Instance.GainExperience(experienceGiven);
    }

}
