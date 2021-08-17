using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MobRespawningHandler : MonoBehaviour
{
    private int k = 0;
    private EnemyHealth health = null;
    private Enemy enemyRef = null;
    private RoamingEnemyBehaviour behaviour = null;
    public List<Transform> resPoints = new List<Transform>();
    private Dictionary<Transform, int> resNumberTracker = new Dictionary<Transform, int>();
    private System.Random rand = new System.Random(); 
    private void Start()
    {
        health = GetComponent<EnemyHealth>();
        behaviour = GetComponent<RoamingEnemyBehaviour>();
        enemyRef = GetComponent<Enemy>();
        foreach (Transform point in resPoints)
        {
            resNumberTracker.Add(point, 0);
        }
    }
    private void OnEnable()
    {
        if (k != 0)
        {
            health.Health = health.MaxHealth;
            behaviour.chasingDistance = behaviour.initialChaisngDistance;
            behaviour.isAllowedToAttack = true;
            transform.position = new Vector3(resPoints[rand.Next(resPoints.Count)].position.x, resPoints[rand.Next(resPoints.Count)].position.y, 0);
            enemyRef.initialPosition = transform.position;
            enemyRef.ManageRoamingPoints();
        }
    }
    private void OnDisable()
    {
        k++;
        if (EnemyTargetManager.Instance.target != null)
        {
            EnemyTargetManager.Instance.target = null;
        }
    }
}
