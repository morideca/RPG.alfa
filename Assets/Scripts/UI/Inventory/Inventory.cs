using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<AssetItem> Items;
    [SerializeField]
    private InventoryCell inventoryCellTemplate;
    [SerializeField]
    private Transform container;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Transform draggingParent;

    [SerializeField]
    private GameObject actionMenu;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private TMP_Text textHealth;
    [SerializeField]
    private TMP_Text textMana;
    [SerializeField]
    private TMP_Text textLevel;
    [SerializeField]
    private TMP_Text textExp;
    [SerializeField]
    private TMP_Text textEssence;

    private ManaManager manaManager;
    private HealthManagerPlayer healthManager;
    private ExpManager expManager;
    private EssenceManager essenceManager;

    private string selectedItem;
    private GameObject selectedCellItem;
    private List<AssetItem> itemData;
    
    private bool isActive = false;

    private void Start()
    {
        manaManager = player.GetComponent<ManaManager>();
        healthManager = player.GetComponent<HealthManagerPlayer>();
        expManager = player.GetComponent<ExpManager>();
        essenceManager = player.GetComponent<EssenceManager>();
    }

    public void OnEnable()
    {
        Render(Items);
        Item.PickedUpItem += AddItem;
        itemData = GetComponent<ItemData>().Items;
        ExpManager.LevelUp += UpdateStats;
        StatManager.StatUp += UpdateStats;
    }

    public void OnDisable()
    {
        Item.PickedUpItem -= AddItem;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Play()
    {
        Time.timeScale = 1;
    }

    public void Render(List<AssetItem> items)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        items.ForEach(item =>
        {
            var cell = Instantiate(inventoryCellTemplate, container);
            cell.Init(draggingParent);
            cell.Render(item);

            cell.Ejecting += () => Destroy(cell.gameObject);
            cell.ActionMenuOn += (string name, GameObject go) =>
            {
                actionMenu.SetActive(true);
                actionMenu.transform.position = Input.mousePosition;
                selectedItem = name;
                selectedCellItem = go;
            };
        });
    }

    public void UseSecelctedItem()
    {
        Debug.Log("1");
        foreach(AssetItem item in itemData)
        {
            Debug.Log(item.name);
            Debug.Log(selectedItem);
            if (item.name == selectedItem)
            {
                Debug.Log("3");
                item.Use();
                Destroy(selectedCellItem);
                UpdateStats();
                actionMenu.SetActive(false);
            }
        }
    }

    private bool MouseIn(RectTransform originalParent)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(originalParent, Input.mousePosition);
    }

    private void CloseActionMenu()
    {
        if (!MouseIn((RectTransform)actionMenu.transform))
        {
            actionMenu.SetActive(false);
        }
    }

    public void AddItem(AssetItem item)
    {
        Items.Add(item);
        Render(Items);
    }

    private void UpdateStats()
    {
        textLevel.text = "LEVEL " + expManager.currentLevel;
        textExp.text = expManager.currentExp + "/" + expManager.expForNextLevel;
        textHealth.text = healthManager.currentHealthPoints + "/" + healthManager.maxHealthPoints;
        textMana.text = manaManager.currentManaPoints.ToString() + "/" + manaManager.maxManaPoints;
        textEssence.text = essenceManager.currentEssenceCount.ToString();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {

            if (isActive == false)
            {
                UpdateStats();
                draggingParent.gameObject.SetActive(true);
                isActive = true;
                Pause();
            }
            else
            {
                draggingParent.gameObject.SetActive(false);
                actionMenu.SetActive(false);
                isActive = false;
                Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CloseActionMenu();
        }

    }
    
}
