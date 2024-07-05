using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum PotionType
{
    healthPotion,
    manaPotion
}
[CreateAssetMenu(fileName = "Potion", menuName = "inventory/items/potion")]
public class PotionItem : AssetItem
{
    [SerializeField]
    PotionType potionType;
    [SerializeField]
    private int amount;


    override public void Use()
    {
        GameObject player = GameObject.FindWithTag("Player");
        switch (potionType)
        {
            case PotionType.healthPotion:
                player.GetComponent<IDamageable>().TakeHeal(amount);
                break;
            case PotionType.manaPotion:
                player.GetComponent<ManaManager>().GetMana(amount);
                break;
        }
    }
}
