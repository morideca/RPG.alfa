using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private GameObject frame;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject[] otherFrames;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _button.SetActive(true);
            
            
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _button.SetActive(false);
         
        }

    }
}
