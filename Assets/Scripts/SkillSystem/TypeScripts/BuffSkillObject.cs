using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// One of three skilll type scripts which are responsible for correct instantiation
/// of Scriptable Object as well as specify skill behaviour.
/// <see cref="ActiveSkillObject"/> for more info.
/// </summary>
[CreateAssetMenu(fileName = "New Buff Skill Object", menuName = "SkillSystem/Skills/Buff")]
public class BuffSkillObject : SkillObject
{
    public ParticleSystem particles;
    public float duration;
    public GameObject buffPrefab;
    public AudioClip sound;
    private void Awake()
    {
        type = SkillType.Buff;
    }
    /// <summary>
    /// Determines what happens when skill on skill bar is pressed twice.
    /// In case of buff-type skills all logic is handled by prefab itself.
    /// Instantiates skill prefab, fires the audio and particles.
    /// </summary>
    public override void UseSkill()
    {
        duration = 3 * level;
        if (buffPrefab != null)
            Instantiate(buffPrefab);
        PlayerSkill.Instance?.ActivateDurationIcon(this);
        var ps = Instantiate(particles, FindObjectOfType<PlayerHealth>().transform.position, Quaternion.identity);
        ps.Play();
        Destroy(ps.gameObject, ps.main.startLifetime.constant);
        SkillAudioEventTrigger.Instance.PlaySound(sound);
    }
}
