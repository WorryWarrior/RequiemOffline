using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFiller : MonoBehaviour
{
    [HideInInspector] public Image image;
    public static ImageFiller Instance { get; private set; }
    private void Awake()
    {
        image = GetComponent<Image>();
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
    private void Update()
    {
        //if (EnemyTargetManager.Instance.target != null)
        //{
            SetCooldown(FireballManager.Instance.cooldown);
        //}
    }
    public void SetCooldown(float cooldown)
    {
        image.fillAmount -= 1.0f / cooldown * Time.deltaTime;
    }
}
