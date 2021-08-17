using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestReward : MonoBehaviour
{
    private TextMeshProUGUI text = null;
    public static QuestReward Instance { get; private set; }

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
    public void SetQuestReward(int reward)
    {
        text.text = "" + reward;
    }
}
