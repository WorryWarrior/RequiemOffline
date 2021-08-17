using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropManager : MonoBehaviour
{
    public GameObject dropPrefab;

    public void OnEnable()
    {
        GetComponent<EnemyHealth>().OnDeath += InstantiateDrop;
    }
    public void OnDisable()
    {
        GetComponent<EnemyHealth>().OnDeath -= InstantiateDrop;
    }
    public void InstantiateDrop()
    {
        var drop = Instantiate(dropPrefab, transform.position, Quaternion.identity);
        drop.GetComponent<Drop>().killedMobName = gameObject.name;
    }
}
