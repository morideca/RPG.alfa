using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanTalk : MonoBehaviour
{
    [SerializeField]
    private string[] text;

    [SerializeField]
    private Image imageClickButton;
    [SerializeField]
    private TMP_Text textClickButton;

    [SerializeField] 
    private KeyCode useButton;

    [SerializeField] 
    private GameObject dialogMenu; 

    private  bool playerIsNear = false;

    private void Start()
    {
        textClickButton.text = useButton.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsNear = true;
            imageClickButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player"))
        {
            playerIsNear = false;
            if (dialogMenu != null) { dialogMenu.SetActive(false); }
            imageClickButton.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(useButton) && playerIsNear)
        {
            dialogMenu.SetActive(true);
            dialogMenu.GetComponent<DialogManager>().StartDialog();
        }
    }
}
