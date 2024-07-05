
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public enum SpellType 
{
    nothing,
    projectTile,
    laser,
    instant,
    teleport
}
 public abstract class SpellBase : MonoBehaviour
{
    [SerializeField]
    public string spellName;

    public int spellId;

    public SpellType spellType;

    public Sprite sprite;
    public Sprite spriteBackground;

    [SerializeField]
    protected Player player;

    [SerializeField]
    protected LayerMask enemyLayer;

    public abstract event Action<float, SpellType> SpellUsed;

    [SerializeField]
    private float range;
    [SerializeField]
    private int manaCost;
    [SerializeField]
    private float cooldownTime;
    [SerializeField]
    private bool onCooldown;

    public int Damage { get; private set; }
    public float Range { get; private set; }
    public int ManaCost { get; private set; }
    public float CooldownTime { get; private set; }
    public bool OnCooldown { get; private set; }

    [SerializeField]
    protected ManaManager manaManager;
    [SerializeField]
    protected SpellManager spellManager;

    protected StatManager statManager;

    protected ExpManager expManager;

    private void Awake()
    {
        Range = range;
        ManaCost = manaCost;
        CooldownTime = cooldownTime;
        OnCooldown = onCooldown;
        statManager = FindObjectOfType<StatManager>();
        expManager = FindObjectOfType<ExpManager>();
        StatManager.StatUp += CountSpellStats;
        CountSpellStats();
    }

    private void CountSpellStats()
    {
        switch (spellType)
        {
            case SpellType.laser:
                Damage = 5 + (int)(statManager.intelligence * 0.25f);
                break;
            case SpellType.projectTile:
                Damage = 10 + (int)(statManager.intelligence * 0.75f);
                break;
            case SpellType.teleport:
                break;
        }
    }

    protected float DistanceToPlayer()
    {
        float distanceToPlayer;
        distanceToPlayer = Vector2.Distance(MousePosition(), (Vector2)transform.position);
        return distanceToPlayer;
    }

    protected Vector2 Direction()
    {
        Vector2 direction;
        direction = ((Vector2)MousePosition() - (Vector2)transform.position).normalized;
        return direction;
    }

    protected Vector2 MousePosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
    }

    protected void UseMana(int manaCost)
    {
        manaManager.UseMana(manaCost);
    }

    protected IEnumerator StartCooldownTimer(float cooldownTime)
    {
        OnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        OnCooldown = false;
    }
    public abstract void Use();

}

 
  



