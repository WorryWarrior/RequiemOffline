using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnnouncementManager : MonoBehaviour
{
    private TextMeshProUGUI announcement = null;
    [SerializeField] private float visibilityTime = 2;
    private bool isSupposedToDisplay = true;
    private Transform player = null;
    public static AnnouncementManager Instance { get; private set; }
   
    private void Awake()
    {
        announcement = GetComponent<TextMeshProUGUI>();
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private IEnumerator FadeAnnouncement(float _1)
    {
        yield return new WaitForSeconds(visibilityTime);
        announcement.enabled = false;
        isSupposedToDisplay = true;
    }
    public void CreateAnnouncement(string text)
    {
        if (isSupposedToDisplay)
        {
            isSupposedToDisplay = false;
            announcement.enabled = true;
            announcement.text = text;
            StartCoroutine(FadeAnnouncement(visibilityTime));
        }
    }

    public void CreateCorrectAnnouncement()
    {
        if (EnemyTargetManager.Instance.target != null)
        {
            if (FireballManager.Instance.isAllowedToFireball)
            {
                if (Vector2.Distance(EnemyTargetManager.Instance.target.transform.position, player.position) < FieldOfView.Instance.viewRadius)
                {
                    if (FireballManager.Instance.mana.Mana - FireballManager.Instance.manaCost >= 0)
                    {
                        //Debug.Log(manager.FetchTarget());
                        if (FireballManager.Instance.FetchTarget())
                        {
                            // Welcome to the mad bit
                        }
                        else
                        {
                            CreateAnnouncement("You must face the target");
                        }
                    }
                    else
                    {
                        CreateAnnouncement("You don't have enough mana");
                    }
                }
                else
                {
                    CreateAnnouncement("You are too far away");
                }
            }
            else
            {
                CreateAnnouncement("This skill is not ready yet");
            }
        }
        else
        {
            CreateAnnouncement("You must choose the target");
        }
    }
}
