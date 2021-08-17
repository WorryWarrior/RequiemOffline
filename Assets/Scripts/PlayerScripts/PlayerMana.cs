using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : GeneralMana
{
    public override void ReduceMana(float cost)
    {
        base.ReduceMana(cost);
        UpdateManaUI();
    }
    public override void RestoreMana(float extraMana)
    {
        base.RestoreMana(extraMana);
        UpdateManaUI();
    }
    public void ModifyMaxMana(int modifier)
    {
        MaxMana = initialMana + modifier;
        UpdateManaUI();
    }
    public void EqualizeMana()
    {
        if (Mana > MaxMana)
        {
            Mana = MaxMana;
        }
        UpdateManaUI();
    }

    public void UpdateManaUI()
    {
        ManaBar.Instance.UpdateManaBar();
        ManaCounter.Instance.UpdatManaCounter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ReduceMana(2);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestoreMana(2);
        }
    }
}
