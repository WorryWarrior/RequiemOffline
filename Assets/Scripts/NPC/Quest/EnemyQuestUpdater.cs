using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyQuestUpdater : MonoBehaviour
{
    private EnemyHealth health = null;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
    }
    private void OnEnable()
    {
        health.OnDeath += UpdateKillBasedQuest;
    }
    private void OnDisable()
    {
        health.OnDeath -= UpdateKillBasedQuest;

    }
    private void UpdateKillBasedQuest()
    {
        for (int i = RequirementTracker.Instance.requirementList.Count - 1; i >= 0; i--)
        {
            var questPrefab = RequirementTracker.Instance.requirementList.Keys.ElementAt(i);
            if (RequirementTracker.Instance.requirementList[questPrefab].Target != null && 
                gameObject.transform.CompareTag(RequirementTracker.Instance.requirementList[questPrefab].Target.tag) &&
                QuestTracker.Instance.questList[QuestTracker.Instance.FetchQuestByName(questPrefab.name)] == (int)QuestTracker.States.InProgress)
            {
                RequirementTracker.Instance.requirementList[questPrefab].CurrentAmount++;
                RequirementTracker.Instance.UpdateQuestProgress(questPrefab);
            }
        }
    }
}
