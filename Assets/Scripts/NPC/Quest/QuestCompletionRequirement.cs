using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompletionRequirement 
{
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }
    public bool TargetReached { get; set; }
    public Transform Target { get; set; }

    public QuestCompletionRequirement(Transform target, int currentAmount, int requiredAmount)
    {
        Target = target;
        CurrentAmount = currentAmount;
        RequiredAmount = requiredAmount;
    }
    public QuestCompletionRequirement(Transform target, bool targetReached)
    {
        Target = target;
        TargetReached = targetReached;
    }
}
