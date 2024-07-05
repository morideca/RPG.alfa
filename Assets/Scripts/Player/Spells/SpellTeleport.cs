using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTeleport : SpellBase
{
    public override event Action<float, SpellType> SpellUsed;

    public static event Action teleportUsed;

    [SerializeField]
    private ParticleSystem spellEffect;


    public override void Use()
    {
        if (OnCooldown != true)
        {
            if (DistanceToPlayer() < Range)
            {
                Instantiate(spellEffect, transform.position, transform.rotation);
                Instantiate(spellEffect, MousePosition(), transform.rotation);
                player.transform.position = MousePosition();
                StartCoroutine(StartCooldownTimer(CooldownTime));
                SpellUsed?.Invoke(CooldownTime, SpellType.teleport);
                teleportUsed?.Invoke();
                manaManager.UseMana(ManaCost);
            }
            else
            {
                Vector2 maxDistance = Direction() * Range;
                Instantiate(spellEffect, transform.position, transform.rotation);
                Instantiate(spellEffect, (Vector2)transform.position + maxDistance, transform.rotation);
                player.transform.position = (Vector2)transform.position + maxDistance;
                StartCoroutine(StartCooldownTimer(CooldownTime));
                SpellUsed?.Invoke(CooldownTime, SpellType.teleport);
                teleportUsed?.Invoke();
                manaManager.UseMana(ManaCost);
            }
        }
    }
}
