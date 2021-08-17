using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMana : MonoBehaviour
{
    [SerializeField] private float mana;
    public float initialMana;
    public float Mana { get => mana; set => mana = value; }
    public float MaxMana { get; set; }
    private void Awake()
    {
        initialMana = Mana;
        MaxMana = Mana;
    }

    public virtual void ReduceMana(float cost)
    {
        Mana -= cost;
        if (Mana <= 0)
        {
            Mana = 0;
        }
    }

    public virtual void RestoreMana(float extraMana)
    {
        Mana += extraMana;
        if (Mana >= MaxMana)
        {
            Mana = MaxMana;
        }
    }
}
