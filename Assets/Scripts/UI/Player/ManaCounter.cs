using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaCounter : MonoBehaviour
{
    private TextMeshProUGUI text;
    private PlayerMana manaRef;
    public static ManaCounter Instance { get; private set; }

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
        manaRef = FindObjectOfType<PlayerMana>();
        text = GetComponent<TextMeshProUGUI>();
    }
    public void UpdatManaCounter()
    {
        text.text = "MP: " + manaRef.Mana + "/" + manaRef.MaxMana;
    }
}
