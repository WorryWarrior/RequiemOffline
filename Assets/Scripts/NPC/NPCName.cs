using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCName : MonoBehaviour
{
    private TextMeshProUGUI text = null;
    private NPC npc = null;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (NPCTracker.Instance.npc != null)
        {
            npc = NPCTracker.Instance.npc.GetComponent<NPC>();
            text.text = npc.npcName;
        }
    }
}
