using CartoonFX;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpellData))]
[RequireComponent(typeof(ManaManager))]
public class SpellManager : MonoBehaviour
{
    [SerializeField]
    protected Image imageSpellQ;
    [SerializeField]
    protected Image imageSpellW;
    [SerializeField]
    protected Image imageSpellE;
    [SerializeField]
    protected Image imageSpellR;

    [SerializeField]
    private GameObject spellQGO;
    [SerializeField]
    private GameObject spellWGO;
    [SerializeField]
    private GameObject spellEGO;
    [SerializeField]
    private GameObject spellRGO;

    [SerializeField]
    private KeyCode spellQ;
    [SerializeField]
    private KeyCode spellW;
    [SerializeField]
    private KeyCode spellE;
    [SerializeField]
    private KeyCode spellR;

    private SpellType spellQType;
    private SpellType spellWType;
    private SpellType spellEType;
    private SpellType spellRType;

    [SerializeField]
    private TMP_Dropdown dropdownQ;
    [SerializeField]
    private TMP_Dropdown dropdownW;
    [SerializeField]
    private TMP_Dropdown dropdownE;
    [SerializeField]
    private TMP_Dropdown dropdownR;

    private List<SpellBase> spells;

    private void Awake()
    {
        spells = GetComponent<SpellData>().spells;
        foreach (var spell in spells)
        {
            spell.SpellUsed += StartImageCooldown;
        }
        SetSpellQ();
        SetSpellW();
        SetSpellE();
        SetSpellR();
    }

    private void Update()
    {
        Spell1();
        Spell2();
        Spell3();
        Spell4();
    }

    public void StartImageCooldown(float cooldownTime, SpellType spellType)
    {
        StartCoroutine(ImageShowCooldown(cooldownTime, spellType));
    }

    private IEnumerator ImageShowCooldown(float cooldownTime, SpellType _spellType)
    {
        if (spellQType == _spellType)
        {
            imageSpellQ.fillAmount = 0;
            while (imageSpellQ.fillAmount < 1)
            {
                imageSpellQ.fillAmount += 1 / cooldownTime * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        else if (spellWType == _spellType)
        {
            imageSpellW.fillAmount = 0;
            while (imageSpellW.fillAmount < 1)
            {
                imageSpellW.fillAmount += 1 / cooldownTime * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        else if (spellEType == _spellType)
        {
            imageSpellE.fillAmount = 0;
            while (imageSpellE.fillAmount < 1)
            {
                imageSpellE.fillAmount += 1 / cooldownTime * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        else if (spellRType == _spellType)
        {
            imageSpellR.fillAmount = 0;
            while (imageSpellR.fillAmount < 1)
            {
                imageSpellR.fillAmount += 1 / cooldownTime * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private void UseSpell(string name)
    {
        foreach (var spell in spells)
        {
            if (spell.spellName == name)
            {
                spell.Use();
            }
        }
    }

    public void SetSpellQ()
    {
        switch (dropdownQ.value) 
        {
            case 0:
                spellQType = SpellType.nothing;
                spellQGO.SetActive(false);
                break;
            case 1:
                spellQGO.SetActive(true);
                spellQType = SpellType.laser;
                foreach (var spell in spells)
                {
                    if (spell.spellName == "laser")
                        imageSpellQ.sprite = spell.sprite;
                }
                break;
            case 2:
                spellQGO.SetActive(true);
                spellQType = SpellType.projectTile;
                foreach (var spell in spells)
                {
                    if (spell.spellName == "bomb")
                        imageSpellQ.sprite = spell.sprite;
                }
                break;
            case 3:
                spellQGO.SetActive(true);
                spellQType = SpellType.teleport;
                foreach (var spell in spells)
                {
                    if (spell.spellName == "teleport")
                        imageSpellQ.sprite = spell.sprite;
                }
                break;
        }
    }

    public void SetSpellW()
    {
        switch (dropdownW.value)
        {
            case 0:
                spellWType = SpellType.nothing;
                spellWGO.SetActive(false);
                break;
            case 1:
                spellWGO.SetActive(true);
                spellWType = SpellType.laser;
                foreach (var spell in spells)
                {
                    if (spell.spellName == "laser")
                        imageSpellW.sprite = spell.sprite;
                }
                break;
            case 2:
                spellWGO.SetActive(true);
                spellWType = SpellType.projectTile;
                foreach (var spell in spells)
                {
                    if (spell.spellName == "bomb")
                        imageSpellW.sprite = spell.sprite;
                }
                break;
            case 3:
                spellWGO.SetActive(true);
                spellWType = SpellType.teleport;
                foreach (var spell in spells)
                {
                    if (spell.spellName == "teleport")
                        imageSpellW.sprite = spell.sprite;
                }
                break;
        }
    }

    public void SetSpellE()
    {
        switch (dropdownE.value)
        {
            case 0:
                spellEType = SpellType.nothing;
                spellEGO.SetActive(false);
                break;
            case 1:
                spellEGO.SetActive(true);
                spellEType = SpellType.laser;
                foreach (var spell in spells)
                {
                    if (spell.spellName == "laser")
                        imageSpellE.sprite = spell.sprite;
                }
                break;
            case 2:
                spellEGO.SetActive(true);
                spellEType = SpellType.projectTile;
                foreach (var spell in spells)
                {
                    if (spell.spellName == "bomb")
                        imageSpellE.sprite = spell.sprite;
                }
                break;
            case 3:
                spellEGO.SetActive(true);
                spellEType = SpellType.teleport;
                foreach (var spell in spells)
                {
                    if (spell.spellName == "teleport")
                        imageSpellE.sprite = spell.sprite;
                }
                break;
        }
    }

    public void SetSpellR()
    {
        switch (dropdownR.value)
        {
            case 0:
                spellRType = SpellType.nothing;
                spellRGO.SetActive(false);
                break;
            case 1:
                spellRGO.SetActive(true);
                spellRType = SpellType.laser;
                foreach (var spell in spells)
                {
                    if (spell.spellName == "laser")
                        imageSpellR.sprite = spell.sprite;
                }
                break;
            case 2:
                spellRGO.SetActive(true);
                spellRType = SpellType.projectTile;
                foreach (var spell in spells)
                {
                    if (spell.spellName == "bomb")
                        imageSpellR.sprite = spell.sprite;
                }
                break;
            case 3:
                spellRGO.SetActive(true);
                spellRType = SpellType.teleport;
                foreach (var spell in spells)
                {
                    if (spell.spellName == "teleport")
                        imageSpellR.sprite = spell.sprite;
                }
                break;
        }
    }

    private void Spell1()
    {
        if (Input.GetKeyDown(spellQ))
        {
            foreach (var spell in spells)
            {
                if (spell.spellType == spellQType)
                {
                    UseSpell(spell.spellName);
                }
            }
        }
        if (Input.GetKeyUp(spellQ))
        {
            foreach (var spell in spells)
            {
                if (spell.spellType == spellQType)
                {
                    UseSpell(spell.spellName);
                }
            }
        }
    }

    private void Spell2()
    {
        if (Input.GetKeyDown(spellW))
        {
            foreach (var spell in spells)
            {
                if (spell.spellType == spellWType)
                {
                    UseSpell(spell.spellName);
                }
            }
        }
        if (Input.GetKeyUp(spellW))
        {
            foreach (var spell in spells)
            {
                if (spell.spellType == spellWType)
                {
                    UseSpell(spell.spellName);
                }
            }
        }
    }
    private void Spell3()
    {
        if (Input.GetKeyDown(spellE))
        {
            foreach (var spell in spells)
            {
                if (spell.spellType == spellEType)
                {
                    UseSpell(spell.spellName);
                }
            }
        }
        if (Input.GetKeyUp(spellE))
        {
            foreach (var spell in spells)
            {
                if (spell.spellType == spellEType)
                {
                    UseSpell(spell.spellName);
                }
            }
        }
    }
    private void Spell4()
    {
        if (Input.GetKeyDown(spellR))
        {
            foreach (var spell in spells)
            {
                if (spell.spellType == spellRType)
                {
                    UseSpell(spell.spellName);
                }
            }
        }
        if (Input.GetKeyUp(spellR))
        {
            foreach (var spell in spells)
            {
                if (spell.spellType == spellRType)
                {
                    UseSpell(spell.spellName);
                }
            }
        }
    }
}