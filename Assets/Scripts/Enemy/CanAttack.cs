using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CanAttack : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackCooldown;
    [SerializeField]
    private float attackDuration;
    

    [SerializeField]
    private bool distanceAttack;

    private bool attackOnCooldown;

    public bool inRangeOfAttack { get; private set; } 
    public bool isAttacking { get; private set; }

    GameObject target;

    private EnemyBase enemyBase;
    private Animator animator;
    private CanMove canMove;



    private void Awake()
    {
        enemyBase = GetComponent<EnemyBase>();
        animator = GetComponent<Animator>();   
        canMove = GetComponent<CanMove>();
        target = enemyBase.player;
        isAttacking = false;
        inRangeOfAttack = false;
    }

    public void Attack()
    {
        if (enemyBase.DistanceToPlayer() <= attackRange)
        {
            inRangeOfAttack = true;
            if (isAttacking == false && attackOnCooldown == false)
            {
                if (distanceAttack == false)
                {
                    StartCoroutine(Attacking());
                    StartCoroutine(TimerAttackCooldown());
                }
                else
                {

                }
            }
        }
        else inRangeOfAttack = false;
    }

    IEnumerator Attacking()
    {
        isAttacking = true;
        animator.SetTrigger("attack");
        target.TryGetComponent(out IDamageable damageable);
        damageable.TakeDamage(damage);
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    IEnumerator TimerAttackCooldown()
    {
        attackOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        attackOnCooldown = false;
    }
}
