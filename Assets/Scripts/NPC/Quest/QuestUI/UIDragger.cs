using UnityEngine;
using UnityEngine.UI;

public class UIDragger : MonoBehaviour
{
    public event System.Action OnQuestStarted;
    public event System.Action OnQuestFinished;
    public GameObject questWindow = null;
    private float offsetX = 0;
    private float offsetY = 0;
    public Button acceptButton = null;
    public Button declineButton = null;
    public Button finishButton = null;
    public Button finishLaterButton = null;
    public void BeginDrag()
    {
        offsetX = transform.position.x - Input.mousePosition.x;
        offsetY = transform.position.y - Input.mousePosition.y;
    }
    public void Drag()
    {
        transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);
    }
    
    public void CloseQuestWindow()
    {
        questWindow.gameObject.SetActive(false);
    }
    public void AcceptQuest()
    {
        OnQuestStarted?.Invoke();
        foreach (Transform obj in QuestSelectHandler.Instance.selectedQuest.questObjectives)
        {
            obj.gameObject.SetActive(true);
        }
        questWindow.gameObject.SetActive(false);
        RequirementTracker.Instance.UpdateQuestRequirementList();
        GameObject.FindGameObjectWithTag("ConversationUI").gameObject.SetActive(false);
        AnnouncementManager.Instance.CreateAnnouncement("You started quest " + QuestSelectHandler.Instance.selectedQuestName);
        QuestTracker.Instance.questList[QuestTracker.Instance.FetchQuestByName(QuestSelectHandler.Instance.selectedQuestName)] = (int)QuestTracker.States.InProgress;
        QuestTracker.Instance.FetchQuestByName(QuestSelectHandler.Instance.selectedQuestName).isAccepted = true;
        QuestTracker.Instance.FetchQuestByName(QuestSelectHandler.Instance.selectedQuestName).gameObject.SetActive(false);
    }
    public void FinishQuest()
    {
        OnQuestFinished?.Invoke();
        questWindow.gameObject.SetActive(false);
        AnnouncementManager.Instance.CreateAnnouncement("You finished quest " + QuestSelectHandler.Instance.selectedQuestName);
        QuestTracker.Instance.questList[QuestTracker.Instance.FetchQuestByName(QuestSelectHandler.Instance.selectedQuestName)] = (int)QuestTracker.States.Finished;
        QuestTracker.Instance.FetchQuestByName(QuestSelectHandler.Instance.selectedQuestName).gameObject.SetActive(false);
        Experience.Instance.GainExperience(QuestTracker.Instance.FetchQuestByName(QuestSelectHandler.Instance.selectedQuestName).quest.Reward);
        QuestTracker.Instance.AdjustQuestLevelVisibility();
    }
    private void OnEnable()
    {
        if (QuestTracker.Instance.FetchQuestByName(QuestSelectHandler.Instance.selectedQuestName) != null &&
            QuestTracker.Instance.questList[QuestTracker.Instance.FetchQuestByName(QuestSelectHandler.Instance.selectedQuestName)] 
            == (int)QuestTracker.States.IsSuitableToFinish)
        {
            acceptButton.gameObject.SetActive(false);
            declineButton.gameObject.SetActive(false);
            finishButton.gameObject.SetActive(true);
            finishLaterButton.gameObject.SetActive(true);
        }
    }
    private void OnDisable()
    {
        acceptButton.gameObject.SetActive(true);
        declineButton.gameObject.SetActive(true);
        finishButton.gameObject.SetActive(false);
        finishLaterButton.gameObject.SetActive(false);
    }
}