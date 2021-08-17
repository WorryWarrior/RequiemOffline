using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawningManager : MonoBehaviour
{
    GameObject player = null;
    [SerializeField] private Transform resPoint = null;
    public ImageFiller skillUI = null;

    public static RespawningManager Instance { get; private set; }
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
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Respawn()
    {
        if (!player.activeSelf)
        {
            AnnouncementManager.Instance.CreateAnnouncement("Rejoice");
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            PlayerMana mana = player.GetComponent<PlayerMana>();
            player.SetActive(true);
            health.Health = health.MaxHealth;
            mana.Mana = mana.MaxMana;
            health.UpdateHealthUI();
            mana.UpdateManaUI();
            player.transform.position = resPoint.position;
        }
    }
}
