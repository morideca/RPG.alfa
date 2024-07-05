using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour, IHaveMana
{
    public int maxManaPoints;
    public int currentManaPoints { get; private set; }
    private int manaRegeneration = 1;

    [SerializeField]
    private Slider manaBar;

    private bool regeneretionOn = true;

    private ExpManager expManager;
    private StatManager statManager;

    private void Start()
    {
        StartCoroutine(ManaRegeneration());
        manaBar.maxValue = maxManaPoints;
        currentManaPoints = maxManaPoints;
        manaBar.value = currentManaPoints;
        expManager = GetComponent<ExpManager>();
        statManager = GetComponent<StatManager>();
        CountHealthStats();
        StatManager.StatUp += CountHealthStats;
        ExpManager.LevelUp += CountHealthStats;

    }

    private void OnDisable()
    {
        StatManager.StatUp -= CountHealthStats;
        ExpManager.LevelUp -= CountHealthStats;
    }

    public void UseMana(int points)
    {
        currentManaPoints -= points;
        if (currentManaPoints < 0) currentManaPoints = 0;
        manaBar.value = currentManaPoints;
    }

    public void GetMana(int points)
    {
        currentManaPoints += points;
        if (currentManaPoints > maxManaPoints)
        {
            currentManaPoints = maxManaPoints;
        }
        manaBar.value = currentManaPoints;
    }

    private IEnumerator ManaRegeneration()
    {
        while (regeneretionOn == true)
        {
            GetMana(manaRegeneration);
            yield return new WaitForSeconds(1f);
        }
    }

    public void IncreaseMaxMana(int amount)
    {
        maxManaPoints += amount;
    }
    public void IncreaseMaxMana(float amount)
    {
        maxManaPoints = (int)(maxManaPoints * amount);
    }

    private void CountHealthStats()
    {
        maxManaPoints = expManager.currentLevel * 5 + statManager.wisdom * 10;
        manaBar.maxValue = maxManaPoints;
        manaRegeneration = (int)Mathf.Floor(statManager.wisdom / 10);
    }
}
