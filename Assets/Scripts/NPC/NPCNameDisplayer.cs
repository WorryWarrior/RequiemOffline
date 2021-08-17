using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCNameDisplayer : MonoBehaviour
{
    private TextMeshPro text = null;
    public NPC NPC = null;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        text.text = NPC.npcName;
    }
}
