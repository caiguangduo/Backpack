using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    /// <summary>
    /// 单例模式
    /// </summary>
    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return instance;
        }
    }

    private List<Item> itemList;

    private ToolTip m_toolTip;
    private ToolTip M_ToolTip
    {
        get
        {
            if (m_toolTip == null)
            {
                m_toolTip = GameObject.FindObjectOfType<ToolTip>();
            }
            return m_toolTip;
        }
    }
    private bool isToolTipShow = false;
    private Vector2 toolTipPositionOffset = new Vector2(30, -55);

    private Canvas m_canvas;
    private Canvas M_Canvas
    {
        get
        {
            if (m_canvas == null)
            {
                m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            }
            return m_canvas;
        }
    }

    private ItemUI m_pickedItem;
    public ItemUI M_PickedItem
    {
        get
        {
            if (m_pickedItem == null)
            {
                m_pickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();
            }
            return m_pickedItem;
        }
    }

    private bool m_isPickedItem = false;
    public bool M_IsPickedItem
    {
        get { return m_isPickedItem; }
        set { m_isPickedItem = value; }
    }

    private void Awake()
    {
        ParseItemJson();
    }

    private void Start()
    {
        M_PickedItem.Hide();
    }

    private void Update()
    {
        if (M_IsPickedItem)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(M_Canvas.transform as RectTransform, Input.mousePosition, null, out position);
            M_PickedItem.SetLocalPosition(position);
        }
        else if (isToolTipShow)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(M_Canvas.transform as RectTransform, Input.mousePosition, null, out position);
            M_ToolTip.SetLocalPosition(position + toolTipPositionOffset);
        }

        if (M_IsPickedItem && Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1) == false)
        {
            M_IsPickedItem = false;
            M_PickedItem.Hide();
        }
    }

    void ParseItemJson()
    {
        itemList = new List<Item>();

        TextAsset itemText = Resources.Load<TextAsset>("Items");
        string itemsJson = itemText.text;

        JSONObject j = new JSONObject(itemsJson);
        Debug.Log(string.Format("{0} {1} {2} {3}", j.IsArray, j.isContainer, j.Count, j.list.Count));

        foreach (JSONObject temp in j.list)
        {
            string typeStr = temp["type"].str;
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), typeStr);

            int id = (int)(temp["id"].n);
            string name = temp["name"].str;
            Item.ItemQuality quality = (Item.ItemQuality)System.Enum.Parse(typeof(Item.ItemQuality), temp["quality"].str);
            string description = temp["description"].str;
            int capacity = (int)(temp["capacity"].n);
            int buyPrice = (int)(temp["buyPrice"].n);
            int sellPrice = (int)(temp["sellPrice"].n);
            string sprite = temp["sprite"].str;

            Item item = null;
            switch (type)
            {
                case Item.ItemType.Consumable:
                    int hp = (int)(temp["hp"].n);
                    int mp = (int)(temp["mp"].n);
                    item = new Consumable(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, hp, mp);
                    break;
                case Item.ItemType.Equipment:
                    int strength = (int)(temp["strength"].n);
                    int intellect = (int)(temp["intellect"].n);
                    int agility = (int)(temp["agility"].n);
                    int stamina = (int)(temp["stamina"].n);
                    Equipment.EquipmentType equipType = (Equipment.EquipmentType)System.Enum.Parse(typeof(Equipment.EquipmentType), temp["equipType"].str);
                    item = new Equipment(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, strength, intellect, agility, stamina, equipType);
                    break;
                case Item.ItemType.Weapon:
                    int damage = (int)(temp["damage"].n);
                    Weapon.WeaponType wpType = (Weapon.WeaponType)System.Enum.Parse(typeof(Weapon.WeaponType), temp["weaponType"].str);
                    item = new Weapon(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, damage, wpType);
                    break;
                case Item.ItemType.Material:
                    item = new Material(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite);
                    break;
            }
            itemList.Add(item);
            //Debug.Log(item);
        }
    }

    public Item GetItemById(int id)
    {
        foreach (Item item in itemList)
        {
            if (item.M_ID == id)
                return item;
        }
        return null;
    }

    public void ShowToolTip(string content)
    {
        if (this.M_IsPickedItem) return;
        isToolTipShow = true;
        M_ToolTip.Show(content);
    }
    public void HideToolTip()
    {
        isToolTipShow = false;
        M_ToolTip.Hide();
    }

    public void PickUpItem(Item item,int amount)
    {
        M_PickedItem.SetItem(item, amount);
        M_IsPickedItem = true;
        M_PickedItem.Show();
        this.M_ToolTip.Hide();
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(M_Canvas.transform as RectTransform, Input.mousePosition, null, out position);
        M_PickedItem.SetLocalPosition(position);
    }

    public void RemoveItem(int amount = 1)
    {
        M_PickedItem.ReduceAmount(amount);
        if (M_PickedItem.M_Amount <= 0)
        {
            M_IsPickedItem = false;
            M_PickedItem.Hide();
        }
    }

    public void SaveInventory()
    {
        Knapsack.M_Instance.SaveInventory();
        Chest.M_Instance.SaveInventory();
        CharacterPanel.M_Instance.SaveInventory();
        Forge.M_Instance.SaveInventory();
        PlayerPrefs.SetInt("CoinAmount", GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CoinAmount);
    }
    public void LoadInventory()
    {
        Knapsack.M_Instance.LoadInventory();
        Chest.M_Instance.LoadInventory();
        CharacterPanel.M_Instance.LoadInventory();
        Forge.M_Instance.LoadInventory();
        if (PlayerPrefs.HasKey("CoinAmount"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CoinAmount = PlayerPrefs.GetInt("CoinAmount");
        }
    }
}
