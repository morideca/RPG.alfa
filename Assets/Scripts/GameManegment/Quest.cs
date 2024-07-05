using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new quest", menuName = "Quests")]
public class Quest : ScriptableObject
{
    public int id;

    public bool questOn;

    public QuestType type;

    public EnemyType enemyTypeTarget;

    public int amount;
    public int targetAmount;

    public string Description;

    public static event Action<int> questFinished;

    public void Progress(EnemyType enemyType = EnemyType.snake)
    {
        switch(type)
        {
            case QuestType.killTarget:
                break;
            case QuestType.killAmount:
                if (enemyType == enemyTypeTarget && questOn == true)
                {
                    amount++;
                    if (amount == targetAmount)
                    {
                        questOn = false;
                        questFinished?.Invoke(id);
                        amount = 0;
                        Debug.Log(amount);
                    }
                }
                break;
            case QuestType.getBless:
                if (questOn)
                {
                    Debug.Log("Quest Progress");
                    questFinished?.Invoke(id);
                    questOn = false;
                }
                break;
            case QuestType.bringAmount:
                break;
        }
    }
    public void Progress()
    {
        if (questOn)
        {
            questFinished?.Invoke(id);
            questOn = false;
        }
    }
}
