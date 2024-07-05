using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName = "new Dialog", menuName = "Dialog")]
public class Dialog : ScriptableObject
    {

    public string[] dialogText;

    public int dialogIndex;

    public string dialogNPC;

    public bool haveQuest;

    public bool questIsFinished;

    public int questId;

    public static event Action<int> startQuest;

    public bool CheckIfQuestIsFinished()
    {
        return FindObjectOfType<QuestManager>().quests[questId].questOn;
    }

    public void StartQuest()
    {
        startQuest?.Invoke(questId);
        Debug.Log("dialog startQuest");
    }
}

