using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    killTarget,
    killAmount,
    bringAmount,
    getBless
}
public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;

    [SerializeField]
    DialogManager dialogManager;

    private void OnEnable()
    {
        Dialog.startQuest += QuestTurnOn;
    }

    private void OnDisable()
    {
        Dialog.startQuest -= QuestTurnOn;
    }

    private void QuestTurnOn(int questId)
    {
        Debug.Log(questId);
        foreach (Quest quest in quests)
        {
            if (quest.id == questId)
            {
                Debug.Log("Found Quest / QUest Mangaer 37");
                quest.questOn = true;
                EnemyBase.enemyKilled += quest.Progress;
                CanBless.blessed += quest.Progress; 
                Quest.questFinished += FinishQuest;
            }
        }
    }

    private void StartQuest(int questID)
    {

    }

    private void FinishQuest(int questId)
    {
      
    }


}
