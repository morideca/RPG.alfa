using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public int vitality = 5;
    public int agility = 5;
    public int armor = 5;
    public int magicResist = 5;
    public int intelligence = 5;
    public int luck = 5;
    public int strenght = 5;
    public int wisdom = 5;

    [SerializeField]
    TMP_Text textVitality;
    [SerializeField]
    TMP_Text textAgility;
    [SerializeField]
    TMP_Text textIntelligence;
    [SerializeField]
    TMP_Text textStrenght;
    [SerializeField]
    TMP_Text textLuck;
    [SerializeField]
    TMP_Text textWisdom;
    [SerializeField]
    TMP_Text textStatPoints;

    private int statPoints;

    public static event Action StatUp;

    private void Awake()
    {
        ExpManager.LevelUp += AddStatPoints;
        textStatPoints.text = "(+" + statPoints.ToString() + ")";
    }

    public void UseStatPoints()
    {
        statPoints--;
        textStatPoints.text = "(+" + statPoints.ToString() + ")";
    }

    public void AddStatPoints()
    {
        statPoints++;
        textStatPoints.text = "(+" + statPoints.ToString() + ")";
    }
    public void AddVitality()
    {
        if (statPoints > 0)
        {
            UseStatPoints();
            vitality++;
            StatUp?.Invoke();
            textVitality.text = vitality.ToString();
        }
    }
    public void AddAgility()
    {
        if (statPoints > 0)
        {
            UseStatPoints();
            agility++;
            StatUp?.Invoke();
            textAgility.text = agility.ToString();
        }
    }
    public void AddArmor()
    {
        if (statPoints > 0)
        {
            UseStatPoints();
            armor++;
            StatUp?.Invoke();
        }
    }
    public void AddMagicReist()
    {
        if (statPoints > 0)
        {
            UseStatPoints();
            magicResist++;
            StatUp?.Invoke();
        }
    }
    public void AddIntelligence()
    {
        if (statPoints > 0)
        {
            UseStatPoints();
            intelligence++;
            StatUp?.Invoke();
            textIntelligence.text = intelligence.ToString();
        }
    }
    public void AddLuck()
    {
        if (statPoints > 0)
        {
            UseStatPoints();
            luck++;
            StatUp?.Invoke();
            textLuck.text = luck.ToString();
        }
    }
    public void AddStrenght()
    {
        if (statPoints > 0)
        {
            UseStatPoints();
            strenght++;
            StatUp?.Invoke();
            textStrenght.text = strenght.ToString();
        }
    }
    public void AddWisdom()
    {
        if (statPoints > 0)
        {
            UseStatPoints();
            wisdom++;
            StatUp?.Invoke();
            textWisdom.text = wisdom.ToString();
        }
    }

    public void AddStatPoints(int count)
    {
        statPoints += count;
        StatUp?.Invoke();
        textStatPoints.text = statPoints.ToString();
    }
    public void AddVitality(int count)
    {
        if (statPoints > 0)
            vitality += count;
        StatUp?.Invoke();
        textVitality.text = vitality.ToString();
    }
    public void AddAgility(int count)
    {
        if (statPoints > 0)
            agility += count;
        StatUp?.Invoke();
        textAgility.text = agility.ToString();
    }
    public void AddArmor(int count)
    {
        if (statPoints > 0)
            armor += count;
        StatUp?.Invoke();
    }
    public void AddMagicReist(int count)
    {
        if (statPoints > 0)
            magicResist += count;
        StatUp?.Invoke();
    }
    public void AddIntelligence(int count)
    {
        if (statPoints > 0)
            intelligence += count;
        StatUp?.Invoke();
        textIntelligence.text = intelligence.ToString();
    }
    public void AddLuck(int count)
    {
        if (statPoints > 0)
            luck += count;
        StatUp?.Invoke();
        textLuck.text = luck.ToString();
    }
    public void AddStrenght(int count)
    {
        if (statPoints > 0)
            strenght += count;
        StatUp?.Invoke();
        textStrenght.text = strenght.ToString();
    }
    public void AddWisdom(int count)
    {
        if (statPoints > 0)
            wisdom += count;
        StatUp?.Invoke();
        textWisdom.text = "(+" + wisdom.ToString() + ")";
    }

}
