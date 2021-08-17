using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName = null;
    public string npcMessage = null;
    public GameObject npcConversationBox = null;
    private PlayerHealth health = null;
    private AudioSource sound = null;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnMouseDown()
    {
        NPCTracker.Instance.npc = gameObject;
        QuestTracker.Instance.AdjustQuestLevelVisibility();
        if (health.Health != 0) 
        {
            if (npcConversationBox.gameObject.activeSelf == false)
            {
                sound.Play();
            }
            
            npcConversationBox.gameObject.SetActive(true);
        }
    }
}
