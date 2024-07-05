using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class BossBase : MonoBehaviour
{
    [SerializeField]
    private float cooldownSpell1;
    [SerializeField]
    private float rangeSpell1;
    [SerializeField]
    private GameObject magicCircle;

    [SerializeField]
    private float cooldownSpell2;
    [SerializeField]
    private float rangeSpell2;
    [SerializeField]
    private float slowPercent;
    [SerializeField]
    private float timeSlow;

    [SerializeField]
    private float cooldownSpell3;
    [SerializeField]
    private Transform createPoint1;
    [SerializeField]
    private Transform createPoint2;
    [SerializeField]
    private Transform createPoint3;
    [SerializeField]
    private GameObject slime;
    [SerializeField]
    private GameObject golem;


    [SerializeField]
    private GameObject spell1Porjectile;

    [SerializeField]
    Transform gun;

    [SerializeField]
    private float flipSpeed;


    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject sprite;


    private bool flipped = false;

    private HealthManager healthManager;
    private Animator animator;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
        animator = GetComponent<Animator>();

        StartCoroutine(CoroutineSpell2());

    }

    public void StartFight()
    {
        StartCoroutine(CoroutineSpell1());
        StartCoroutine(CoroutineSpell2());
        StartCoroutine(CoroutineSpell3());
    }

    private void Update()
    {
        FlipX(player.transform.position);

    }

    public void FlipX(Vector2 target)
    {
        Vector3 rotate;
        if (target.x > transform.position.x && flipped == false)
        {
            rotate = sprite.transform.eulerAngles;
            rotate.y = 180;
            if (sprite.transform.rotation != Quaternion.Euler(rotate))
            {
                sprite.transform.Rotate(0, flipSpeed, 0);
            }
            else flipped = true;
        }

        else if (target.x < transform.position.x && flipped == true) 
        {
            rotate = sprite.transform.eulerAngles;
            rotate.y = 0;
            if (sprite.transform.rotation != Quaternion.Euler(rotate))
            {
                sprite.transform.Rotate(0, -flipSpeed, 0);
            }
            else flipped = false;
        }
    }

    public void Spell1()
    {
        Vector2 randomPoint = (Vector2)player.transform.position + Random.insideUnitCircle * 4;
        Instantiate(magicCircle, gun.position, Quaternion.identity);
        GameObject _bullet = Instantiate(spell1Porjectile, gun.position, Quaternion.identity);
        _bullet.GetComponent<NecromanceSpellProjectile>().SetOptions(randomPoint);
    }

    public void Spell2()
    {
            Debug.Log(timeSlow);
            player.GetComponent<PlayerMove>().Slow(slowPercent, timeSlow);
    }

    public void Spell3()
    {
        Instantiate(slime, createPoint1.position, Quaternion.identity);
        Instantiate(slime, createPoint2.position, Quaternion.identity);
        Instantiate(slime, createPoint3.position, Quaternion.identity);
    }

    

    public Vector2 DirectionToPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        return direction;
    }

    public float DistanceToPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance;
    }

    IEnumerator CoroutineSpell1()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldownSpell1);
            animator.SetTrigger("spell1");
        }
    }    
    
    IEnumerator CoroutineSpell2()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldownSpell2);
            animator.SetTrigger("spell2");
        }
    }

    IEnumerator CoroutineSpell3()
    {
        while (true)
        {
            animator.SetTrigger("spell3");
            yield return new WaitForSeconds(cooldownSpell3);
        }
    }

    public void TakeDamage(int damage)
    {
        healthManager.TakeDamage(damage);
    }

    public void TakeHeal(int heal)
    {
        healthManager.TakeHeal(heal);
    }

   
}

