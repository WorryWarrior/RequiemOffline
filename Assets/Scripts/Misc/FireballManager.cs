using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballManager : MonoBehaviour
{
    public Transform fireballPrefab = null;
    public float fireballCastTime = 1;
    [HideInInspector] public float fireballCooldown = 0;
    public float cooldown = 0;
    [HideInInspector] public bool isAllowedToFireball = true;
    [HideInInspector] public PlayerMana mana;
    public float manaCost = 3;
    public static FireballManager Instance { get; private set; }

    private void Awake()
    {
        mana = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMana>();
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
        fireballCooldown = cooldown;
    }

    private void Update()
    {
        //CastFireball();
    }
    /*private void CastFireball()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AnnouncementManager.Instance.CreateCorrectAnnouncement();
            if (EnemyTargetManager.Instance.target != null && isAllowedToFireball &&
            (Vector2.Distance(EnemyTargetManager.Instance.target.transform.position, transform.position) < FieldOfView.Instance.viewRadius &&
            mana.Mana - manaCost >= 0 && FetchTarget()))
            {
                CastingUI.Instance.SetCastTime(fireballCastTime);
                TopDownMovementScript.Instance.isAllowedToMove = false;
                CastingUI.Instance.isSupposedToBeSeen = true;
                isAllowedToFireball = false;
                StartCoroutine(FireballCast(fireballCastTime));
            }
        }
    }*/

    public bool FetchTarget()
    {
        for (int i = 0; i < FieldOfView.Instance.detectedEnemies.Count; i++)
        {
            if (EnemyTargetManager.Instance.target.transform == FieldOfView.Instance.detectedEnemies[i])
            {
                return true;
            } 
        }
        return false;
    }
    IEnumerator FireballCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        isAllowedToFireball = true;
    }
    IEnumerator FireballCast(float time)
    {
        yield return new WaitForSeconds(time);
        mana.ReduceMana(manaCost);
        StartCoroutine(FireballCooldown(fireballCooldown));
        ImageFiller.Instance.image.fillAmount = 1;
        ImageFiller.Instance.SetCooldown(fireballCooldown);
        Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        TopDownMovementScript.Instance.isAllowedToMove = true;
    }
}
