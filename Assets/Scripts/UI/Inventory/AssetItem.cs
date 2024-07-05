using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Items")]
public class AssetItem : ScriptableObject, IItem
{

    [SerializeField]
    private string name;
    [SerializeField]
    private Sprite uiIcon;
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private int id;

    GameObject IItem.ItemPrefab => itemPrefab;
    int IItem.Id => id;
    string IItem.Name => name;
    Sprite IItem.UIcon => uiIcon;

    public void OnTriggerEnter2D()
    {
    
    }

    public virtual void Use()
    {

    }
}
