using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyName : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (EnemyTargetManager.Instance.target != null)
        {
            text.text = EnemyTargetManager.Instance.target.name;
        } 
    }
}
