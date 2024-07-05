using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulStone : MonoBehaviour
{
    public int exp = 3;
    public int essence = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<ExpManager>().TakeExp(exp);
            other.gameObject.GetComponent<EssenceManager>().TakeEssence(essence);
            Destroy(gameObject);
        }
    }
}
