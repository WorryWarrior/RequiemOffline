using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Determines a behaviour of skill popup that gives basic skill information
/// basing on its type.
/// </summary>
public class SkillPopup : MonoBehaviour
{
    private Image window;
    [SerializeField] private TextMeshProUGUI spellName = null;
    [SerializeField] private Image spellSprite = null;
    [SerializeField] private TextMeshProUGUI levelText = null;
    [SerializeField] private TextMeshProUGUI manacostText = null;
    [SerializeField] private TextMeshProUGUI cooldownText = null;
    [SerializeField] private TextMeshProUGUI castText = null;
    [SerializeField] private TextMeshProUGUI passiveDescriptionText = null;
    [SerializeField] private TextMeshProUGUI activeDescriptionText = null;

    private void Awake()
    {
        window = GetComponent<Image>();
    }
    private void Start()
    {
        SetComponentsInvisible();
    }
    /// <summary>
    /// Adjusts window's position.
    /// </summary>
    public void UpdatePosition(Vector3 position, float xOffset, float yOffset)
    {
        var panel = window.GetComponent<RectTransform>();
        panel.anchoredPosition = new Vector3(position.x + xOffset, position.y + yOffset);
    }
    /// <summary>
    /// Sets certain components visible basing on the type of analyzed skill.
    /// </summary>
    /// <param name="_skillObj">Analyzed skill.</param>
    public void SetComponentsVisible(SkillObject _skillObj)
    {
        window.enabled = true;
        spellName.enabled = true;
        spellSprite.enabled = true;
        levelText.enabled = true;
        if (_skillObj.type != SkillType.Passive)
        {  
            manacostText.enabled = true;
            cooldownText.enabled = true;
            castText.enabled = true;
            activeDescriptionText.enabled = true;
        } 
        else
            passiveDescriptionText.enabled = true;
        UpdateSkillValues(_skillObj);
    }
    /// <summary>
    /// Sets components invisible.
    /// </summary>
    public void SetComponentsInvisible()
    {
        window.enabled = false;
        spellName.enabled = false;
        spellSprite.enabled = false;
        levelText.enabled = false;
        manacostText.enabled = false;
        cooldownText.enabled = false;
        castText.enabled = false;
        passiveDescriptionText.enabled = false;
        activeDescriptionText.enabled = false;
    }
    /// <summary>
    /// Updates certain components basing on the type of analyzed skill.
    /// </summary>
    /// <param name="_skillObj">Analyzed skill.</param>
    public void UpdateSkillValues(SkillObject _skillObj)
    {
        spellName.text = _skillObj.data.Name;
        spellSprite.sprite = _skillObj.activeSprite;
        levelText.text = "Lv." + _skillObj.level;
        if (_skillObj.type != SkillType.Passive)
        {
            manacostText.text = "MP Consumption: " + _skillObj.manacost;
            cooldownText.text = "Skill Downtime: " + _skillObj.cooldown + " Sec";
            activeDescriptionText.text = _skillObj.description;
            if (_skillObj.castTime != 0)
                castText.text = "Casting Time: " + _skillObj.castTime + " Sec";
            else
                castText.text = "Casting Time: Instant Cast";
        } else
            passiveDescriptionText.text = _skillObj.description;
    }
}
