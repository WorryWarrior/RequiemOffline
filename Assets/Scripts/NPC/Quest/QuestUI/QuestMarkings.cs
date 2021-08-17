using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarkings : MonoBehaviour
{
    public GameObject available = null;
    public GameObject inProgress = null;
    public GameObject completeable = null;
    //public GameObject associatedNPC = null;
    private int minLevelRequirement = 100;
    private List<QuestPrefab> associatedQuests = new List<QuestPrefab>();

    private void Start()
    {
        foreach (QuestPrefab quest in FindObjectsOfType<QuestPrefab>())
        {
            if (quest.questGiver.name == gameObject.name)
            {
                associatedQuests.Add(quest);
            }
            
        }
        foreach (QuestPrefab quest in associatedQuests)
        {
            if (quest.levelRequirement < minLevelRequirement)
            {
                minLevelRequirement = quest.levelRequirement;
            }
        }
    }
    private void Update()
    {
        if (QuestSelectHandler.Instance.selectedQuest != null && QuestSelectHandler.Instance.selectedQuest.isLastInSequence &&
            QuestTracker.Instance.questList[QuestTracker.Instance.FetchQuestByName(QuestSelectHandler.Instance.selectedQuest.name)] ==
            (int)QuestTracker.States.Finished && Experience.Instance.level >= minLevelRequirement &&
            associatedQuests.Contains(QuestSelectHandler.Instance.selectedQuest.nextQuestInSequence))
        {
            available.SetActive(true);
        }
        else
        {
            DisplayProperMark();
        }

    }
    public void DisplayProperMark()
    {
        foreach (QuestPrefab quest in associatedQuests)
        {
            if (QuestTracker.Instance.questList[quest] == 1)
            {
                available.SetActive(true);
                completeable.SetActive(false);
                gameObject.GetComponent<QuestMarkings>().enabled = true;
            }
            if (QuestTracker.Instance.questList[quest] == 2)
            {
                available.SetActive(false);
                inProgress.SetActive(true);
            }
            if (QuestTracker.Instance.questList[quest] == 3)
            {
                inProgress.SetActive(false);
                completeable.SetActive(true);
            }
            if (CalculateStateQuestAmount((int)QuestTracker.States.Available) == 0 &&
                CalculateStateQuestAmount((int)QuestTracker.States.InProgress) == 0 &&
                CalculateStateQuestAmount((int)QuestTracker.States.IsSuitableToFinish) == 0 &&
                CalculateStateQuestAmount((int)QuestTracker.States.Unavailable) != 0)
            {
                available.SetActive(false);
                inProgress.SetActive(false);
                completeable.SetActive(false);
            }
            if (CalculateStateQuestAmount((int)QuestTracker.States.Finished) == associatedQuests.Count)
            {
                available.SetActive(false);
                inProgress.SetActive(false);
                completeable.SetActive(false);
                gameObject.GetComponent<QuestMarkings>().enabled = false;
            }
        }
    }
    private int CalculateStateQuestAmount(int state)
    {
        var counter = 0;
        foreach (QuestPrefab quest in associatedQuests)
        {
            if (QuestTracker.Instance.questList[quest] == state)
            {
                counter++;
            }
        }
        return counter;
    }
}
