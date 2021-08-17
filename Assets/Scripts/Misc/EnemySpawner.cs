using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawningPoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Transform mob = Instantiate(enemyPrefab, spawningPoint.position, Quaternion.identity);
            mob.name = enemyPrefab.name;
        }
    }
}
