using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawningHandler : MonoBehaviour
{
    private PlayerHealth health = null;
    private FireballManager manager = null;
    private Character damageRef = null;
    [SerializeField] private Experience playerExp = null;
    [SerializeField] private float experienceLossPercentage = 0;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        manager = GetComponent<FireballManager>();
        damageRef = GetComponent<Character>();
    }
    private void OnEnable()
    {
        health.OnPlayerDeath -= ResetValues;
    }
    private void OnDisable()
    {
        health.OnPlayerDeath += ResetValues;
    }

    private void ResetValues()
    {
        damageRef.isAllowedToAttack = true;
        playerExp.LoseExperiencePercentage(experienceLossPercentage);
        EnemyTargetManager.Instance.target = null;
    }
}
