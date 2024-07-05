using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ExpManager : MonoBehaviour
{
    [SerializeField]
    ParticleSystem levelUpEffect;

    [SerializeField]
    Transform transformLevelUpEffect;

    public int currentExp { get; private set; }
    public int expForNextLevel { get; private set; }
    public int currentLevel { get; private set;}

    public static event Action LevelUp;

    private void Awake()
    {
        currentExp = 0;
        currentLevel = 1;
        CountExpForNextLevel();
    }

    public void TakeExp(int exp)
    {
        currentExp += exp;
        if (currentExp >= expForNextLevel)
        {
            int excessExp = expForNextLevel - currentExp;
            GetLevelUp();
            currentExp = exp;
            if (currentExp >= expForNextLevel) TakeExp(currentExp);
        }
    }

    private void GetLevelUp()
    {
        LevelUp?.Invoke();
        currentLevel++;
        CountExpForNextLevel();
        Instantiate(levelUpEffect, transformLevelUpEffect);
    }

    public void GetLevelUp(int count)
    {
        LevelUp?.Invoke();
        currentLevel += count;
        currentExp = 0;
        CountExpForNextLevel();
        Instantiate(levelUpEffect, transformLevelUpEffect);
    }

    private void CountExpForNextLevel()
    {
        expForNextLevel = currentLevel * 10;
    }
}
