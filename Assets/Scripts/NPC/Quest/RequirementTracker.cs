using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementTracker : MonoBehaviour
{
    public static RequirementTracker Instance { get; private set; }
    private AudioSource sound = null;

    public Dictionary<QuestPrefab, QuestCompletionRequirement> requirementList = new Dictionary<QuestPrefab, QuestCompletionRequirement>();
    private void Awake()
    {
        sound = GetComponent<AudioSource>();
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
    public void UpdateQuestRequirementList()
    {
        foreach (QuestPrefab quest in FindObjectsOfType<QuestPrefab>())
        {
            requirementList.Add(quest, quest.requirement);
        }
    }
    public void UpdateQuestProgress(QuestPrefab quest)
    {
        AnnouncementManager.Instance.CreateAnnouncement(quest.announcementNotification+ " "+requirementList[quest].CurrentAmount + " / " +
                requirementList[quest].RequiredAmount);
        if (requirementList[quest].CurrentAmount >= requirementList[quest].RequiredAmount)
        {
            sound.Play();
            requirementList[quest].CurrentAmount = requirementList[quest].RequiredAmount;
            QuestTracker.Instance.questList[quest] = (int)QuestTracker.States.IsSuitableToFinish;
        }
    }

    public QuestPrefab FetchQuestByName(string nameToLookFor)
    {
        foreach (QuestPrefab listedQuest in requirementList.Keys)
        {
            if (listedQuest.name == nameToLookFor)
            {
                return listedQuest;
            }
        }
        return null;
    }
}
