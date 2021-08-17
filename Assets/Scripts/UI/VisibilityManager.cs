using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisibilityManager : MonoBehaviour
{
    public EnemyHealthBar bar;
    public EnemyName text;
    public AnnouncementManager announcement;
    public PlayerHealth health;
    public Button button;

    private void Update()
    {
        if (EnemyTargetManager.Instance.target != null)
        {
            bar.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
        }
        else
        {
            bar.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
        }
        if (health.Health <= 0)
        {
            button.gameObject.SetActive(true);
        }
        else
        {
            button.gameObject.SetActive(false);
        }
    }
}
