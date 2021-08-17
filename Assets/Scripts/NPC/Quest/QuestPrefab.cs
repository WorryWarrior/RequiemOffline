using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestPrefab : MonoBehaviour
{
    private TextMeshProUGUI QuestTitle = null;
    public GameObject questWindow = null;
    [HideInInspector] public Quest quest = null;
    [HideInInspector] public QuestCompletionRequirement requirement = null;
    [SerializeField] private string title = "";
    [TextArea(7,8)] [SerializeField] private string description = "";
    [TextArea(7,8)] [SerializeField] private string finishingDescription = "";
    [SerializeField] private int reward = 0;
    public int levelRequirement = 0;
    public QuestPrefab previousQuestInSequence = null;
    public QuestPrefab nextQuestInSequence = null;
    [HideInInspector] public bool isAccepted = false;
    public Transform questTarget = null;
    public int currentAmount = 0;
    public int targetAmountRequirement = 0;
    public string announcementNotification = null;
    public Transform[] questObjectives;
    public GameObject questGiver = null;
    public bool isLastInSequence = false;
    private void Awake()
    {
        QuestTitle = GetComponent<TextMeshProUGUI>();
        quest = new Quest(title, description, finishingDescription ,reward);
        requirement = new QuestCompletionRequirement(questTarget, currentAmount, targetAmountRequirement);
        QuestTitle.text = title;
        gameObject.name = quest.QuestName;
    }
    public void Select()
    {
        QuestTitle.color = Color.blue;//new Color(105, 165, 255);
        questWindow.gameObject.SetActive(true);
        QuestSelectHandler.Instance.selectedQuest = this;
        QuestSelectHandler.Instance.selectedQuestName = quest.QuestName;
        QuestName.Instance.SetQuestName(quest.QuestName);
        if (QuestTracker.Instance.questList[QuestTracker.Instance.FetchQuestByName(quest.QuestName)] == (int)QuestTracker.States.Available)
        {
            QuestDescription.Instance.text.enableAutoSizing = true;
            QuestDescription.Instance.SetQuestDescription(quest.QuestDescription);
        }
        if (QuestTracker.Instance.questList[QuestTracker.Instance.FetchQuestByName(quest.QuestName)] == (int)QuestTracker.States.IsSuitableToFinish)
        {
            if (finishingDescription.Length > 75) 
            {
                QuestDescription.Instance.text.enableAutoSizing = true; 
            } else
            {
                QuestDescription.Instance.text.enableAutoSizing = false;
                QuestDescription.Instance.text.fontSize = 20;
            }
            
            QuestDescription.Instance.SetQuestDescription(quest.QuestFinishingDescription);
        }
        QuestReward.Instance.SetQuestReward(quest.Reward);
    }
    private void Update()
    {
        if(QuestSelectHandler.Instance.selectedQuestName != quest.QuestName 
            || QuestSelectHandler.Instance.selectedQuestName == null
            || questWindow.activeSelf == false)
            QuestTitle.color = Color.white;
    }
}
