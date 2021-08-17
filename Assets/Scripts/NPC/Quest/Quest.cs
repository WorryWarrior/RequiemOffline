using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Quest
{
    public string QuestName { get; set; }
    public string QuestDescription { get; set; }
    public int Reward { get; set; }
    public string QuestFinishingDescription { get; set; }
    public Quest(string questName, string questDescription, string questFinishingDescription, int reward)
    {
        QuestName = questName;
        QuestDescription = questDescription;
        QuestFinishingDescription = questFinishingDescription;
        Reward = reward;
    }
}
