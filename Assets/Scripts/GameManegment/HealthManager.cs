using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour, IDamageable
{
    public int maxHealthPoints;
    // will be private, when i`ll use dataBase
    public int currentHealthPoints { get; protected set; }
    [SerializeField]
    protected int healthRegeneration = 1;

    [SerializeField]
    protected SpriteRenderer sprite;

    [SerializeField]
    protected Slider healthBar;
    //private int armor;
    //private int magicResist;

    protected bool regeneretionOn = true;

    public void Awake()
    {
        StartCoroutine(HealthRegeneration());
        healthBar.maxValue = maxHealthPoints;
        currentHealthPoints = maxHealthPoints;
        healthBar.value = currentHealthPoints;
    }
    public void TakeDamage(int damage)
    {
        currentHealthPoints -= damage;
        if (currentHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
        if (gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(HealthBarOnDamage());
        }
        StartCoroutine(WasDamagedColorIndecator());
        healthBar.value = currentHealthPoints;
    }

    public void TakeHeal(int heal)
    {
        currentHealthPoints += heal;
        if (currentHealthPoints > maxHealthPoints) 
        { 
            currentHealthPoints = maxHealthPoints; 
        }
        healthBar.value = currentHealthPoints;
    }

    public IEnumerator HealthRegeneration()
    {
        while (regeneretionOn == true)
        {
            TakeHeal(healthRegeneration);
            yield return new WaitForSeconds(1f);
        }
    }
    public IEnumerator HealthBarOnDamage()
    {
        healthBar.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        healthBar.gameObject.SetActive(false);
    }

    public IEnumerator WasDamagedColorIndecator()
    {
        while (sprite.color != Color.red)
        {
            sprite.color = Color.Lerp(sprite.color, Color.red, 1f);
        }
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.Lerp(sprite.color, Color.white, 1);
    }
    public void IncreaseMaxHealth(int amount)
    {
        maxHealthPoints += amount;
    }
    public void IncreaseMaxHealth(float amount)
    {
        maxHealthPoints = (int)(maxHealthPoints * amount);
    }
}
