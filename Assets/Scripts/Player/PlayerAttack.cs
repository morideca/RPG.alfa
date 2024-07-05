using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private int damage;

    [SerializeField]
    private float attackRange;
    private float bulletSpeed;
    private float attackSpeed;

    private bool attackOnCooldown = false;

    [SerializeField]
    private GameObject bullet;

    public LayerMask layerMaskEnemy;

    private StatManager statManager;

    private ExpManager expManager;

    private void OnEnable()
    {
        ExpManager.LevelUp += CountAttackStats;
        StatManager.StatUp += CountAttackStats;
    }


    private void OnDisable()
    {
        ExpManager.LevelUp -= CountAttackStats;
        StatManager.StatUp -= CountAttackStats;
    }

    private void Awake()
    {
        statManager = GetComponent<StatManager>();
        expManager = GetComponent<ExpManager>();
        CountAttackStats();
    }
    private void Update()
    {
        RightClickAttack();
    }

    private void RightClickAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && attackOnCooldown == false)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject _bullet = Instantiate(bullet, transform.position, Quaternion.identity);
            _bullet.GetComponent<PlayerBullet>().WithTarget(mousePosition, bulletSpeed, 
                attackRange, damage);
            StartCoroutine(StartCooldownTimer());
        }
    }

    public void IncreaseAttackSpeed(float multiplier)
    {
        attackSpeed *= multiplier;
    }

    public void CountAttackStats()
    {
        damage = 1 * expManager.currentLevel + 1 * statManager.strenght;
        attackSpeed = 1 - (expManager.currentLevel / 100) - (statManager.vitality / 100);
        bulletSpeed = 3 + statManager.vitality / 10;
    }

    private IEnumerator StartCooldownTimer()
    {
        attackOnCooldown = true;
        yield return new WaitForSeconds(1 / attackSpeed);
        attackOnCooldown = false;
    }
}
