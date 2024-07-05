using Unity.VisualScripting;
using UnityEngine;


public interface IItem
{
    public int Id { get; }
    public string Name { get; }
    public Sprite UIcon { get; }
    public GameObject ItemPrefab { get; }
    void OnTriggerEnter2D();
}
