using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnCombatDamage : MonoBehaviour
{
    private TextMeshPro text;
    [SerializeField] private float slidingSpeed = 0;
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }
    public void CreateText(float damage)
    {
        text.text = damage.ToString();
        Destroy(gameObject, 0.5f);
    }
    private void Update()
    {
        transform.position += new Vector3(0, slidingSpeed) * Time.deltaTime;
    }
}
