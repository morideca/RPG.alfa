using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerBullet : MonoBehaviour
{
    private int damage = 5;
    private float bulletSpeed = 5;
    private float rangeAttack = 10;
    private float lifeTime;

    private Vector2 direction;
    
    [SerializeField]
    ParticleSystem effect;

    private void Awake()
    {
        lifeTime = rangeAttack / bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<HealthManager>().TakeDamage(damage);
            Instantiate(effect, other.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void WithTarget(Vector2 target, float bulletSpeed, float rangeAttack, int damage)
    {
        this.rangeAttack = rangeAttack;
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
        this.direction = (target - (Vector2)transform.position).normalized;
    }

    public void WithoutTarget(Vector2 mousePosition, float bulletSpeed, float rangeAttack, int damage)
    {
        this.rangeAttack = rangeAttack;
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
        this.direction = (mousePosition - (Vector2)transform.position).normalized;
    }
    public void Start()
    {
        StartCoroutine(LifeTime());
    }

    void Update()
    {
            transform.position = Vector3.MoveTowards(transform.position, 
                (Vector2)transform.position + direction, bulletSpeed * Time.deltaTime);
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject );
    }
}
