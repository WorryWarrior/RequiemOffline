using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTracker : MonoBehaviour
{
    public GameObject npcConversationBox = null;
    public GameObject questBox = null;
    public static NPCTracker Instance { get; private set; }

    public GameObject npc;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        npc = GameObject.FindGameObjectWithTag("Background");
    }

    public void SetConversationInactive()
    {
        npcConversationBox.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (FindObjectOfType<TopDownMovementScript>() != null &&
            Vector2.Distance(FindObjectOfType<TopDownMovementScript>().transform.position, npc.transform.position) > 10f)
        {
            SetConversationInactive();
            questBox.gameObject.SetActive(false);
        }
    }
}
