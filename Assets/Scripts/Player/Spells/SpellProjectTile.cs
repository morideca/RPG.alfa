using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PojectTileType
{
    instantAoE,
    throwSpell
}
public class SpellProjectTile : SpellBase
{
    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private GameObject attackRadiusGameObject;
    [SerializeField] 
    ParticleSystem spellEffect;
    [SerializeField]
    protected GameObject spellPreview;

    public PojectTileType pojectTileType;

    private bool preUseOn;

    private Coroutine startPreUse;

    public override event Action<float, SpellType> SpellUsed;


    private void Start()
    {
        spellPreview.transform.localScale = new Vector3(explosionRadius * 2, explosionRadius * 2, 0);
        attackRadiusGameObject.transform.localScale = new Vector3(Range * 2, Range * 2, 0);
        PreviewOff();
    }

    private void DealDamage(Vector3 origin)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(origin, explosionRadius, enemyLayer);
        foreach (Collider2D other in colliders)
        {
            if (!other.gameObject.CompareTag("Player") &&
                other.gameObject.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(Damage);
            }
        }
    }

    public override void Use()
    {
        if (preUseOn == true)
        {
            StopCoroutine(startPreUse);
            preUseOn = false;
            if (OnCooldown == false && manaManager.currentManaPoints >= ManaCost)
            {
                SpellUsed?.Invoke(CooldownTime, spellType);
                PreviewOff();
                switch (pojectTileType)
                {
                    case PojectTileType.instantAoE:
                        DistanceToPlayer();
                        if (DistanceToPlayer() < Range)
                        {
                            Instantiate(spellEffect, MousePosition(), transform.rotation);
                            DealDamage(MousePosition());
                            StartCoroutine(StartCooldownTimer(CooldownTime));
                        }
                        else
                        {
                            Vector2 maxDistance = Direction() * Range;
                            Instantiate(spellEffect, (Vector2)transform.position + maxDistance, transform.rotation);
                            DealDamage((Vector2)transform.position + maxDistance);
                            StartCoroutine(StartCooldownTimer(CooldownTime));
                        }
                        break;
                    case PojectTileType.throwSpell:
                        break;
                }
                UseMana(ManaCost);
            }
        }
        else startPreUse = StartCoroutine(PreUse());
    }

    public IEnumerator PreUse()
    {
        preUseOn = true;
        Debug.Log(DistanceToPlayer());
        Debug.Log(Range);
        while (true)
        {
            if (OnCooldown == false && manaManager.currentManaPoints >= ManaCost)
            {
                PreviewOn();
                if (DistanceToPlayer() < Range)
                {
                    Debug.Log(MousePosition());
                    Debug.Log(DistanceToPlayer());
                    spellPreview.transform.position = MousePosition();
                }
                else
                {
                    Vector2 maxDistance = Direction() * Range;
                    spellPreview.transform.position = (Vector2)transform.position + maxDistance;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void PreviewOn()
    {
        attackRadiusGameObject.SetActive(true);
        spellPreview.SetActive(true);
    }
    private void PreviewOff()
    {
        attackRadiusGameObject.SetActive(false);
        spellPreview.SetActive(false);
    }
}
