using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpdateTriggerQuest : MonoBehaviour
{
    public float allowedTriggeringDistance = 3f;
    public float triggeringTime = 1.25f;
    public QuestPrefab targetQuest = null;
    private bool isTriggering = false;
    public GameObject popup = null;
    private void Update()
    {
        UpdateTriggerBasedQuest();
    }
    private void UpdateTriggerBasedQuest()
    {
        for (int i = RequirementTracker.Instance.requirementList.Count - 1; i >= 0; i--)
        {
            var questPrefab = RequirementTracker.Instance.requirementList.Keys.ElementAt(i);
            if (FindObjectOfType<TopDownMovementScript>() != null &&
                Vector2.Distance(gameObject.transform.position, FindObjectOfType<TopDownMovementScript>().transform.position) < allowedTriggeringDistance &&
                QuestTracker.Instance.questList[QuestTracker.Instance.FetchQuestByName(questPrefab.name)] == (int)QuestTracker.States.InProgress)
            {
                Triggering();
            }
        }
    }
    private void Triggering()
    {
        if (Input.GetMouseButtonDown(1) && !isTriggering)
        {
            isTriggering = true;
            FindObjectOfType<TopDownMovementScript>().isAllowedToMove = false;
            CastingUI.Instance.SetCastTime(triggeringTime);
            CastingUI.Instance.isSupposedToBeSeen = true;
            StartCoroutine(TriggeringFinisher(triggeringTime));
        }
    }
    private IEnumerator TriggeringFinisher(float delay)
    {
        yield return new WaitForSeconds(delay);
        FindObjectOfType<TopDownMovementScript>().isAllowedToMove = true;
        RequirementTracker.Instance.requirementList[targetQuest].CurrentAmount++;
        RequirementTracker.Instance.UpdateQuestProgress(targetQuest);
        Destroy(gameObject);
        isTriggering = false;
    }
    private void OnMouseEnter()
    {
        CursorHandler.Instance.SetTriggerCursor();
        popup.SetActive(true);
    }
    private void OnDisable()
    {
        CursorHandler.Instance.SetDefaultCursor();
        popup.SetActive(false);
    }
    private void OnMouseExit()
    {
        CursorHandler.Instance.SetDefaultCursor();
        popup.SetActive(false);
    }
}
