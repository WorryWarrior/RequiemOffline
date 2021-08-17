using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFinisherHandler : MonoBehaviour
{
    public GameObject questWindow = null;
    private UIDragger script = null;
    private AudioSource sound;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        script = questWindow.GetComponent<UIDragger>();
    }

    private void OnEnable()
    {
        script.OnQuestFinished += FinishQuest;
    }
    private void OnDisable()
    {
        script.OnQuestFinished -= FinishQuest;
    }
    private void FinishQuest()
    {
        sound.Play();
    }
}
