using System;
using TMPro;
using UnityEngine;

public enum Bless
{
    moveSpeedUp,
    healthUp,
    manaUp,
    attackSpeedUp,
    getNewSpell
}

public class CanBless : MonoBehaviour
{
    private GameObject player;

    private KeyCode useKey = KeyCode.F;

    private int cost = 5;
    private int useCount = 0;

    private bool inZone = false;

    [SerializeField]
    ParticleSystem blessEffect;

    [SerializeField]
    private TMP_Text textCost;

    [SerializeField]
    private GameObject textGO;

    private Bless bless;

    private EssenceManager essenceManager;

    public static event Action blessed;

    private void Awake()
    {
        textCost.text = cost.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            textCost.text = cost.ToString();
            textGO.SetActive(true);
            inZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textGO.SetActive(false);
            inZone = false;
        }
    }


    public void BlessIt()
    {
        Instantiate(blessEffect, player.transform.position, Quaternion.identity);
        blessed?.Invoke();
        switch (bless)
        {
            case Bless.attackSpeedUp:
                Debug.Log("attack speed up");
                player.GetComponent<PlayerAttack>().IncreaseAttackSpeed(1.25f);
                break;
            case Bless.manaUp:
                Debug.Log("mana up");
                player.GetComponent<ManaManager>().IncreaseMaxMana(1.1f);
                break;
            case Bless.healthUp:
                Debug.Log("health up");
                player.GetComponent<HealthManager>().IncreaseMaxHealth(1.1f);
                break;
            case Bless.moveSpeedUp:
                Debug.Log("movespeed up");
                player.GetComponent<PlayerMove>().IncreaseMoveSpeed(1.5f);
                break;
            case Bless.getNewSpell:
                Debug.Log("#%#");
                break;
        }
    }

    private void SetNextBless()
    {
        switch (useCount)
        {
            case 0:
                bless = Bless.manaUp;
                break;
            case 1:
                bless = Bless.healthUp;
                break;
            case 2:
                bless = Bless.attackSpeedUp;
                break;
            case 3:
                bless = Bless.moveSpeedUp;
                break;
            case 4:
                bless = Bless.getNewSpell;
                break;
        }
        useCount += 1;
    }


    public void SetCostText()
    {
        textCost.text = cost.ToString();
    }

    private void Use()
    {
        if (player.GetComponent<EssenceManager>().currentEssenceCount >= cost)
        {
            player.GetComponent<EssenceManager>().spentEssence(cost);
            BlessIt();
            SetNextBless();
            cost = (int)(cost * 1.5f);
            textCost.text = cost.ToString();    
        }
        else
        {
            textCost.text = "No";
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(useKey) && inZone == true)
        {
            Use();
        }
    }
}
