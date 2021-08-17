using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetName : MonoBehaviour
{
    private TextMeshPro text;
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }
    public void Display(string textToSpawn)
    {
        text.text = textToSpawn;
    }
    public void Update()
    {
        if (EnemyTargetManager.Instance.target == null)
        {
            TargetNameManager.Instance.isActive = false;
            Destroy(gameObject);
        }
    }
}
