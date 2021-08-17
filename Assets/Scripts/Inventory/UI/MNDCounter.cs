using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// One of three singleton classes responsible for displaying correct amount of character stats.
/// </summary>
public class MNDCounter : MonoBehaviour
{
    private TextMeshProUGUI text;
    private PlayerInventory stat;
    public static MNDCounter Instance { get; private set; }
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        stat = FindObjectOfType<PlayerInventory>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Updates the text to the relevant state.
    /// </summary>
    public void UpdateMindCounter()
    {
        text.text = "MND: " + stat.attributes[stat.GetAttributeID(Attributes.Mind)].value.ModifiedValue;
    }
}
