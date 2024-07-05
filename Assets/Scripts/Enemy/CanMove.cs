using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public enum MoveType
{
    directly,
    zigZag,
    shift,
    shase,
    teleport,
    stay
}

public class CanMove : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float agrDistance;
    [SerializeField]
    private float shiftCooldownTime;
    [SerializeField]
    private float shiftPower;

    private GameObject target;

    private Rigidbody2D rigidbody;

    [SerializeField]
    private CircleCollider2D agrZone;

    private Animator animator;

    private EnemyBase enemyBase;

    private CanAttack canAttack;

    [SerializeField]
    SpriteRenderer unitSprite;

    [SerializeField] 
    LayerMask layer1;

    [SerializeField]
    private MoveType moveType;

    [SerializeField]
    private bool idleMovingOn;

    private Coroutine startShifting;
    private Coroutine idleMoving;
    private bool shifting = false;

    private bool agrOn = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        agrZone = GetComponentInChildren<CircleCollider2D>();
        animator = GetComponent<Animator>();
        enemyBase = GetComponent<EnemyBase>();  
        canAttack = GetComponent<CanAttack>();
        target = enemyBase.player;
        agrZone.radius = agrDistance;
        idleMoving = StartCoroutine(IdleMoving(transform.position));
    }



    public void Move()
    {
        if (agrOn && canAttack.isAttacking == false)
        {
            switch (moveType)
            {
                case MoveType.directly:
                    if (canAttack.inRangeOfAttack == false)
                    {
                        animator.SetBool("isMoving", true);
                        transform.position = Vector3.MoveTowards(transform.position,
                            target.transform.position, speed * Time.deltaTime);
                    }
                    break;
                case MoveType.zigZag:

                    break;
                case MoveType.stay:
                    break;
                case MoveType.shift:
                    {
                        if (shifting == false)
                        {
                           shifting = true;
                           startShifting = StartCoroutine(Shifting());
                        }
                    }
                    break;
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, enemyBase.DirectionToPlayer(),
                agrDistance * 2, layer1);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                agrOn = true;
                FlipX(collision.gameObject.transform.position);
            }
            else
            {
                agrOn = false;
                StopAllCoroutines();
                if (idleMovingOn == true)
                {
                    idleMoving = StartCoroutine(IdleMoving(transform.position));
                }
                shifting = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            agrOn = false;
            StopAllCoroutines();
            shifting = false;
            if (idleMovingOn == true)
            {
                idleMoving = StartCoroutine(IdleMoving(transform.position));
            }
        }
    }

    private IEnumerator IdleMoving(Vector2 startPosition)
    {
        while (agrOn == false)
        {
            float timer = 0;
            float x = Random.Range(-2, 2) + startPosition.x;
            float y = Random.Range(-2, 2) + startPosition.y;
            Vector2 targetPoint = new Vector2(x, y);
            while (timer <= 3)
            {
                transform.Translate((targetPoint - (Vector2)transform.position).normalized * Time.deltaTime);
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        StopCoroutine(idleMoving);
    }

    private IEnumerator Shifting()
    {
        while (agrOn)
        {
            yield return new WaitForSeconds(2);
            rigidbody.AddForce(enemyBase.DirectionToPlayer().normalized * shiftPower);
            yield return new WaitForSeconds(shiftCooldownTime);
        }
        StopCoroutine(startShifting);
    }

    public void FlipX(Vector2 target)
    {
        if (target.x > transform.position.x)
        {
            unitSprite.flipX = true;
        }
        else { unitSprite.flipX = false; }
    }
}
