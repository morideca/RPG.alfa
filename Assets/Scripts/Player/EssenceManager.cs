using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceManager : MonoBehaviour
{
    public int currentEssenceCount { get; private set; }

    private void Awake()
    {
        currentEssenceCount = 0;
    }
    public void TakeEssence(int count)
    {
        currentEssenceCount += count;
    }

    public void spentEssence(int count)
    {
        currentEssenceCount -= count; 
    }
}
