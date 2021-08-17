using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    [HideInInspector] public int level = 1;
    [HideInInspector] public float experience = 0;
    [HideInInspector] public float experienceRequirement = 200;
    public float experienceMultiplier = 1.5f;
    public ExperienceUI text;
    public ExperienceBar expBar;
    private AudioSource sound;

    public PlayerHealth health;
    public PlayerMana mana;
    public event System.Action OnLevelGained;

    public static Experience Instance { get; private set; }
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
    }
    private void Start()
    {
        sound = GetComponent<AudioSource>();
        UpdateExpUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GainExperience(100);
        }
    }
    public void ChangeExperienceModifier(float value)
    {
        experienceMultiplier = 1 + value;
    }
    public void GainExperience(int amount)
    {
        experience += amount * experienceMultiplier;
        if (experience >= experienceRequirement)
        {
            OnLevelGained?.Invoke();
        }
        UpdateExpUI();
    }
    public void LoseExperience(int amount)
    {
        experience -= amount;
        if (experience <= 0)
        {
            experience = 0;
        }
        UpdateExpUI();
    }
    public void LoseExperiencePercentage(float percentage)
    {
        experience *= percentage;
        UpdateExpUI();
    }

    public void UpdateExpUI()
    {
        text.UpdateText();
        expBar.UpdateExperienceUI();
        LevelCounter.Instance.UpdateText();
    }
    private void GainLevel()
    {
        level++;

        PlayerSkill.Instance?.GainSkillPoint();
        experience -= experienceRequirement;
        experienceRequirement += 250;
        health.MaxHealth += 2;
        mana.MaxMana += 5;
        health.initialHealth += 2;
        mana.initialMana += 5;
        health.Health = health.MaxHealth;
        mana.Mana = mana.MaxMana;
        QuestTracker.Instance.AdjustQuestLevelVisibility();
        health.UpdateHealthUI();
        mana.UpdateManaUI();
        foreach (EnemyHealth enemy in FindObjectsOfType<EnemyHealth>())
        {
            if (enemy.Health == enemy.MaxHealth)
            {
                enemy.MaxHealth = level * 8;
                enemy.Health = enemy.MaxHealth;
            }
        }
        sound.Play();
    }
    private void OnEnable()
    {
        OnLevelGained += GainLevel;
    }
    private void OnDisable()
    {
        OnLevelGained -= GainLevel;
    }
}
