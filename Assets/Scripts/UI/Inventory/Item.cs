using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] 
    private AssetItem item;
    public static event Action <AssetItem> PickedUpItem;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickedUpItem?.Invoke(item);
            Destroy(gameObject);
        }
    }
    public void OnTrigger2DEnter(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickedUpItem?.Invoke(item);
            Destroy(gameObject);
        }
    }
}
