using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSelectHandler : MonoBehaviour
{
    public QuestPrefab selectedQuest = null;
    public string selectedQuestName = null;
    public static QuestSelectHandler Instance { get; private set; }

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
    }
}
