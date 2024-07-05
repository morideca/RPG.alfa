using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public enum EnemyType
{
    snake,
    golem,
    warrior,
    necromancer,
    slime
}
[RequireComponent (typeof(CanMove))]
[RequireComponent (typeof(CanAttack))]
public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int expForKill = 1;
    [SerializeField]
    protected int essenceForKill = 1;


    protected HealthManager healthManager;
    protected CanMove canMove;
    protected CanAttack canAttack;

    public EnemyType enemyType;

    [SerializeField]
    public GameObject player;

    [SerializeField]
    protected GameObject soulGameObject;

    public static event Action<EnemyType> enemyKilled;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
        canAttack = GetComponent<CanAttack>();
        canMove = GetComponent<CanMove>();
        player = FindFirstObjectByType<Player>().gameObject;
    }

    public float DistanceToPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance;
    }

    public Vector2 DirectionToPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        return direction;
    }

    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded)
            return;
        enemyKilled?.Invoke(enemyType);
        GameObject soul = Instantiate(soulGameObject, transform.position, Quaternion.identity);
        soul.GetComponent<SoulStone>().exp = expForKill;
        soul.GetComponent<SoulStone>().essence = essenceForKill;

    }

    public void TakeDamage(int damage)
    {
        healthManager.TakeDamage(damage);
    }

    public void TakeHeal(int heal)
    {
        healthManager.TakeHeal(heal);
    }

    public virtual void Update()
    {
        canMove.Move();
        canAttack.Attack();
    }
}
