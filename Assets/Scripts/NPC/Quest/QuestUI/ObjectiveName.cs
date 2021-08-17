using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveName : MonoBehaviour
{
    private TextMeshPro text = null;
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        if (ObjectivePopup.Instance.selectedObjective != null)
           text.text = ObjectivePopup.Instance.selectedObjective.GetComponent<UpdateTriggerQuest>().targetQuest.questTarget.name;
    }
}
