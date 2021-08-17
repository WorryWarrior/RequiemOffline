using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestName : MonoBehaviour
{
    private TextMeshProUGUI text = null;
    public static QuestName Instance { get; private set; }

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
    public void SetQuestName(string name)
    {
        text.text = name;
    }
}
