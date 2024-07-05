using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private string[] dialogData;

    [SerializeField]
    private TMP_Text dialogText;
    [SerializeField]
    private float speedText;

    private Coroutine print;

    private int indexPhrase = 0;
    private int indexDialog = 0;

    private bool endOfDialog;

    private bool questIsDone;

    private DialogDataBase dialogDataBase;

    private void Awake()
    {
        dialogDataBase = GetComponent<DialogDataBase>();
        endOfDialog = false;
        questIsDone = true;

        Quest.questFinished += FinishedQuest;
    }

    private void OnDisable()
    {
        indexPhrase = 0;
        endOfDialog = false;
        if (print != null) StopAllCoroutines();
    }

    public void StartDialog()
    {
        StopAllCoroutines();
        dialogText.text = string.Empty;
        indexPhrase = 0;
        if (indexDialog == dialogDataBase.dialogData.Count)
        {
            print = StartCoroutine(Print(dialogData));
        }
        else
        {
            dialogData = dialogDataBase.dialogData[indexDialog].dialogText;
            print = StartCoroutine(Print(dialogData));
        }
    }

    private void SkipDialogText()
    {
        if (dialogText.text == dialogData[indexPhrase])
        {
            if (indexPhrase == dialogData.Length - 1)
            {
                if (dialogDataBase.dialogData[indexDialog].haveQuest == true)
                {
                    dialogDataBase.dialogData[indexDialog].StartQuest();
                    questIsDone = false;
                    if (indexDialog != dialogDataBase.dialogData.Count - 1)
                    indexDialog++;
                }
                if (questIsDone == true && indexDialog != dialogDataBase.dialogData.Count - 1)
                {
                    indexDialog++;
                }
                    gameObject.SetActive(false);
            }
            else
            {
                NextLine();
            }
        }
        else
        {
            if (print != null)
            {
                StopCoroutine(print);
            }
            dialogText.text = dialogData[indexPhrase];
        }
    }


    IEnumerator Print(string[] dialogData)
    {
        foreach (char i in dialogData[indexPhrase].ToCharArray())
        {
            dialogText.text += i;
            yield return new WaitForSeconds(speedText);
        }
    }

    private void NextLine()
    {
        indexPhrase++;
        dialogText.text = string.Empty;
        if (indexPhrase <= dialogData.Length - 1)
        {
            print = StartCoroutine(Print(dialogData));
        }
    }

    private void FinishedQuest(int questID)
    {
        Debug.Log("DialogManager Finished quest");
        indexDialog++;
        questIsDone = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SkipDialogText();
            if (endOfDialog) gameObject.SetActive(false);
        }
    }
}
