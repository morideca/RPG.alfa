using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyIt());
    }

    private IEnumerator DestroyIt()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
