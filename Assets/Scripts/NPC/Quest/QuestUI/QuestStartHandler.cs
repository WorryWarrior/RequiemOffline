using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStartHandler : MonoBehaviour
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
        script.OnQuestStarted += StartQuest;
    }
    private void OnDisable()
    {
        script.OnQuestStarted -= StartQuest;
    }
    private void StartQuest()
    {
        sound.Play();
    }
}
