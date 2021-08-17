using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCounter : MonoBehaviour
{
    private TextMeshProUGUI text;
    public static LevelCounter Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText()
    {
        text.text = "" + Experience.Instance.level;
    }
}
