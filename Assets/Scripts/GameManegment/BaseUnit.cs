using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BaseUnit : MonoBehaviour
{

    protected float maxManaPoint = 100;
    protected float manaPoint;
    protected float manaRegeneration = 1;
    protected float speed = 5;
    protected int essence = 0;
    protected int exp;
    protected int expForLevel = 10;
    protected int level = 1;

    protected Vector3 mousePosition1;
    protected Vector3 moveDirection;

    public Rigidbody2D unitRigidbody;

    [SerializeField]
    protected GameObject bullet;
    [SerializeField]
    protected LayerMask layer1;
    [SerializeField]
    protected Slider manaBar;
    [SerializeField]
    protected Animator animator;

    Coroutine coroutine;

    public void TakeExp(int expCount)
    {
        exp += expCount;
        if (exp == expForLevel)
        {
            LevelUp();
            exp = 0;
        }
        if (exp > expForLevel)
        {
            exp -= expForLevel;
            LevelUp();
            TakeExp(exp);
        }

    }

    public virtual void LevelUp()
    {

    }

    public virtual void Soul()
    {

    }




    //public void healthManaBar()
    //{
    //    healthBar.value = healthPoint;
    //    manaBar.value = manaPoint;
    //}



    //public void ManaRegeneration()
    //{
    //    if (regenerationTimer >= 3 && manaPoint <= maxManaPoint)
    //    {
    //        manaPoint += manaRegeneration;
    //        if (manaPoint > maxManaPoint)
    //        {
    //            manaPoint = maxManaPoint;
    //        }
    //    }
    //}

}
