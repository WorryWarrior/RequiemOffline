using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for changing the state of skill window arrows.
/// Contains a single method that is called throughout the code.
/// </summary>
public class ArrowHandler : MonoBehaviour
{
    public Sprite activeArrow;
    public Sprite inactiveArrow;
    public SkillObject skillToTrack;
    public void UpdateArrowState()
    {
        if (skillToTrack.level != 0)
        {
            GetComponent<Image>().sprite = activeArrow;
        }
        else
        {
            GetComponent<Image>().sprite = inactiveArrow;
        }
    }
}
