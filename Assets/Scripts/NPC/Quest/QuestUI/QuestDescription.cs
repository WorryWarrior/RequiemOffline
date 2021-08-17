using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestDescription : MonoBehaviour
{
    [HideInInspector] public TextMeshProUGUI text = null;
    public static QuestDescription Instance { get; private set; }

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
    public void SetQuestDescription(string name)
    {
        text.text = name;
    }
}
