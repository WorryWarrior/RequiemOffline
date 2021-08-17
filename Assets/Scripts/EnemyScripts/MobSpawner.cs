using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    static System.Random rnd = new System.Random();
    public List<Transform> spawningPoints = new List<Transform>();
    public Transform enemyPrefab = null;
    private EnemyHealth health;
    private int randomNum = 0;
    private List<string> names = new List<string> { "Helitard", "Mata Pato", "Funky Mongoloid", "Valac", "Baker" };

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
    }
    private void OnEnable()
    {
        health.OnDeath += SpawnMob;
    }

    private void OnDisable()
    {
        health.OnDeath -= SpawnMob;
    }

    private void SpawnMob()
    {
        randomNum = Random.Range(0, spawningPoints.Count);
        Vector3 fixedPosition = new Vector3(spawningPoints[randomNum].position.x, spawningPoints[randomNum].position.y, 0);
        Transform mob = Instantiate(enemyPrefab, fixedPosition, Quaternion.identity);
        int num = rnd.Next(names.Count);
        mob.name = names[num];
    }
}
