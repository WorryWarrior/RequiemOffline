using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask enemyMask;
    [HideInInspector] public List<Collider2D> targetsInViewRadius = new List<Collider2D>();
    [HideInInspector] public List<Transform> detectedEnemies = new List<Transform>();
    public static FieldOfView Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        StartCoroutine("DetectEnemies", 0.2f);
    }

    public IEnumerator DetectEnemies(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    private void FindVisibleTargets()
    {
        detectedEnemies.Clear();
        ContactFilter2D cf = new ContactFilter2D();
        cf.SetLayerMask(enemyMask);
        Physics2D.OverlapCircle(transform.position, viewRadius, cf, targetsInViewRadius);
        for (int i = 0; i < targetsInViewRadius.Count; i++)
        {
            if (targetsInViewRadius[i] != null)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.right, directionToTarget) < viewAngle / 2)
                {
                    detectedEnemies.Add(target);
                }
            }
        }

    }
    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }
    public bool FetchTarget()
    {
        for (int i = 0; i < detectedEnemies.Count; i++)
        {
            if (EnemyTargetManager.Instance.target.transform == detectedEnemies[i])
            {
                return true;
            }
        }
        return false;
    }
}
