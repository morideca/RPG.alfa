using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromanceSpellProjectile : MonoBehaviour
{
    Vector2 direction;
    Vector3 target;

    [SerializeField]
    GameObject targetArea;
    private GameObject targetAreaGO;

    int damage = 30;

    [SerializeField]
    private ParticleSystem effect;


    public void SetOptions(Vector3 playerPosition)
    {
        direction = ((Vector2)playerPosition - (Vector2)transform.position).normalized;
        target = playerPosition;
        targetAreaGO = Instantiate(targetArea, target, Quaternion.identity);
        Rotate();
    }

    private void Rotate()
    {
        var angle = Mathf.Atan2(direction.y, direction.x);
        angle = Mathf.Rad2Deg * angle;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 3 * Time.deltaTime);
        if (transform.position == target)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    collider.GetComponent<HealthManagerPlayer>().TakeDamage(damage);
                }
            }
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(targetAreaGO);
            Destroy(gameObject);
        }

    }

}
