using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpdateWalkingQuests : MonoBehaviour
{
    public Transform destination = null;
    public QuestPrefab questTrigger;

    private void Update()
    {
        UpdateWalkBasedQuest();
    }
    private void UpdateWalkBasedQuest()
    {
        for (int i = RequirementTracker.Instance.requirementList.Count - 1; i >= 0; i--)
        {
            var questPrefab = RequirementTracker.Instance.requirementList.Keys.ElementAt(i);
            if (FindObjectOfType<TopDownMovementScript>() != null && 
                Vector2.Distance(destination.position, FindObjectOfType<TopDownMovementScript>().transform.position) < 1f &&
                QuestTracker.Instance.questList[QuestTracker.Instance.FetchQuestByName(questPrefab.name)] == (int)QuestTracker.States.InProgress)
            {
                RequirementTracker.Instance.requirementList[questPrefab].CurrentAmount++;
                RequirementTracker.Instance.UpdateQuestProgress(questPrefab);
            }
            
            if (QuestTracker.Instance.questList[QuestTracker.Instance.FetchQuestByName(questPrefab.name)] == (int)QuestTracker.States.InProgress &&
                questTrigger != questPrefab)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
