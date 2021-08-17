using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    public static QuestTracker Instance { get; private set; }

    public Dictionary<QuestPrefab, int> questList = new Dictionary<QuestPrefab, int>();
    public enum States { Unavailable = -1, Available = 1, InProgress = 2, IsSuitableToFinish = 3, Finished = 4 }

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
        foreach (QuestPrefab quest in FindObjectsOfType<QuestPrefab>())
        {
            if (quest.levelRequirement > Experience.Instance.level)
            questList.Add(quest, (int)States.Unavailable);
            else
            {
                questList.Add(quest, (int)States.Available);
            }
        }
    }
    public QuestPrefab FetchQuestByName(string nameToLookFor)
    {
        foreach (QuestPrefab listedQuest in questList.Keys)
        {
            if (listedQuest.name == nameToLookFor)
            {
                return listedQuest;
            }
        }
        return null;
    }
    public void MakeQuestAvailable(string questName)
    {
        QuestPrefab desirableQuest = FetchQuestByName(questName);
        if (desirableQuest != null)
        {
            questList[desirableQuest] = (int)States.Available;
        }
        else
        {
            Debug.Log("Such quest isn't in the database");
        }
    }
    public void AdjustQuestLevelVisibility()
    {
        for (int i = questList.Count - 1; i >= 0; i--)
        {
            var questPrefab = questList.Keys.ElementAt(i);
            if ((!questPrefab.isAccepted && questPrefab.previousQuestInSequence == null || 
                questPrefab.levelRequirement <= Experience.Instance.level && questList[questPrefab] == (int)States.Unavailable
                && questList[questPrefab.previousQuestInSequence] == (int)States.Finished) && questPrefab.questGiver == NPCTracker.Instance.npc)
            {
                questList[questPrefab] = (int)States.Available;
                questPrefab.gameObject.SetActive(true);
            }
            if (questList[questPrefab] == (int)States.Unavailable || questPrefab.questGiver != NPCTracker.Instance.npc)
            {
                questPrefab.gameObject.SetActive(false);
            }
            if (questList[questPrefab] == (int)States.IsSuitableToFinish && questPrefab.questGiver == NPCTracker.Instance.npc)
            {
                questPrefab.gameObject.SetActive(true);
            }
        }
    }
    private void OnEnable()
    {
        if (NPCTracker.Instance != null) 
        {
            AdjustQuestLevelVisibility();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            for (int i = questList.Count - 1; i >= 0; i--)
            {
                var questPrefab = questList.Keys.ElementAt(i);
                if (questList[questPrefab] == (int)States.InProgress)
                {
                    questList[questPrefab] = (int)States.IsSuitableToFinish;
                }
            }
            foreach (QuestPrefab quest in questList.Keys)
            {
                Debug.Log("Quest's " + quest.name + " state is " + questList[quest]);
            }
        }
    }
}
