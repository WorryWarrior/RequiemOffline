using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetNameManager : MonoBehaviour
{
    [SerializeField] private Transform textPrefab = null;
    [HideInInspector] public bool isActive = false;
    private Transform text;
    Vector3 position;
    public static TargetNameManager Instance { get; private set; }
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
    public void DisplayName()
    {
        if (EnemyTargetManager.Instance.target != null && !isActive)
        {
            textPrefab.GetComponent<TextMeshPro>().color = Color.red;
            textPrefab.GetComponent<TextMeshPro>().fontStyle = FontStyles.Bold;
            text = Instantiate(textPrefab, position, Quaternion.identity);
            isActive = true;
        }
    }
    private void Update()
    {
        if (EnemyTargetManager.Instance.target != null)
        {
            TargetName textSpawner = text.GetComponent<TargetName>();
            textSpawner.Display(EnemyTargetManager.Instance.target.name);
            position.Set(EnemyTargetManager.Instance.target.transform.position.x,
                            EnemyTargetManager.Instance.target.transform.position.y + 1.2f, EnemyTargetManager.Instance.target.transform.position.z);
            text.position = position;
        }
    }
}
