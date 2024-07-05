using System.Collections;
using UnityEngine;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public enum LaserType
{
    penetrate,
    firstTarget
}
public class SpellLaser : SpellBase
{
    [SerializeField]
    private GameObject laser;

    [SerializeField]
    private LaserType laserType;

    private Coroutine laserOn;
    private Coroutine startAim;

    [SerializeField]
    EGA_Laser laserEffect;

    public override event Action<float, SpellType> SpellUsed;

    public override void Use()
    {
        if (laserOn != null)
        {
            TurnOff();
        }
        else if (manaManager.currentManaPoints >= ManaCost && OnCooldown == false)
        {
            TurnOn();
        }
    }

    public void TurnOn()
    {
        laserEffect.MaxLength = Range;
        startAim = StartCoroutine(Aim());
        laserOn = StartCoroutine(LaserOn());
    }

    public void TurnOff()
    {
        laser.SetActive(false);
        StopCoroutine(laserOn);
        laserOn = null;
        StartCoroutine(StartCooldownTimer(CooldownTime));
        StopCoroutine(startAim);
        SpellUsed?.Invoke(CooldownTime, spellType);
    }

    private IEnumerator Aim()
    {
        while(true)
        {
            Vector2 vectorToTarget = MousePosition() - (Vector2)transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            laser.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return new WaitForFixedUpdate();
        }

    }

    private IEnumerator LaserOn()
    {
        while (true)
        {
            switch (laserType)
            {
                case LaserType.firstTarget:
                    Debug.Log(1);
                    laser.SetActive(true);
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction(), Range, enemyLayer);
                    if (hit.collider != null && !hit.collider.gameObject.CompareTag("Player") &&
                        hit.collider.gameObject.TryGetComponent(out HealthManager target))
                    {
                        target.TakeDamage(Damage);
                    }
                    break;

                case LaserType.penetrate:
                    laser.SetActive(true);
                    RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Direction(), Range, enemyLayer);
                    foreach (RaycastHit2D _hit in hits)
                    {
                        if (_hit.collider != null && !_hit.collider.gameObject.CompareTag("Player") &&
                        _hit.collider.gameObject.TryGetComponent(out IDamageable _target))
                        {
                            _target.TakeDamage(Damage);
                        }
                    }
                    break;

            }
            UseMana(ManaCost);
            if (manaManager.currentManaPoints < ManaCost) TurnOff();
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
}
