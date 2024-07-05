using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float shiftSpeedUp = 1.33f;
    private float moveSpeed;
    private float baseMoveSpeed = 4;
    private float additionalMoveSpeed;

    [SerializeField]
    private KeyCode up;
    [SerializeField]
    private KeyCode down;
    [SerializeField]
    private KeyCode left;
    [SerializeField]
    private KeyCode right;

    public Coroutine goingTo;

    protected bool shiftOn = false;

    private Animator animator;

    [SerializeField]
    private SpriteRenderer sprite;

    public bool canMove;

    [SerializeField]
    ParticleSystem slowEffect;

    private StatManager statManager;


    private void Awake()
    {
        statManager = GetComponent<StatManager>();
        animator = GetComponent<Animator>();
        CountMoveStats();
        canMove = true;
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (Input.GetKey(up))
        {
            transform.Translate((Vector2.up * moveSpeed * Time.deltaTime));
        }
        if (Input.GetKey(down))
        {
            transform.Translate((Vector2.down * moveSpeed * Time.deltaTime));
        }
        if (Input.GetKey(left))
        {
            transform.Translate((Vector2.left * moveSpeed * Time.deltaTime));
            sprite.flipX = true;
        }
        if (Input.GetKey(right))
        {
            transform.Translate((Vector2.right * moveSpeed * Time.deltaTime));
            sprite.flipX = false;
        }
    }

    private void OnEnable()
    {
        StatManager.StatUp += CountMoveStats;
    }

    private void OnDisable()
    {
        StatManager.StatUp -= CountMoveStats;
    }

    private void CountMoveStats()
    {
        additionalMoveSpeed = (statManager.agility * 0.05f);
        moveSpeed = baseMoveSpeed + additionalMoveSpeed;
    }

    public void Slow(float multiply, float time)
    {
        StartCoroutine(SlowDown(multiply, time));
        Debug.Log(time);
    }
    private IEnumerator SlowDown(float slowPercent, float time)
    {
        Instantiate(slowEffect, transform.position, Quaternion.identity);
        float moveSpeedDifference = moveSpeed * (slowPercent / 100);
        moveSpeed -= moveSpeedDifference;
        yield return new WaitForSecondsRealtime(time);
        moveSpeed += moveSpeedDifference;
    }

    public void IncreaseMoveSpeed(float amount)
    {
        moveSpeed *= amount;
    }


}

