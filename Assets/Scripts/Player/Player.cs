using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private int skillpoint;
    private HealthManager healthManager;
    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
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