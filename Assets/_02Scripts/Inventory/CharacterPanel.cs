using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterPanel : Inventory
{
    private static CharacterPanel m_instance;
    public static CharacterPanel M_Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.Find("CharacterPanel").GetComponent<CharacterPanel>();
            }
            return m_instance;
        }
    }

    private Player m_player;
    private Player M_Player
    {
        get
        {
            if (m_player == null)
            {
                m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }
            return m_player;
        }
    }

    private Text m_propertyText;
    private Text M_PropertyText
    {
        get
        {
            if (m_propertyText == null)
            {
                m_propertyText = transform.Find("PropertyPanel/Text").GetComponent<Text>();
            }
            return m_propertyText;
        }
    }

    private void Start()
    {
        UpdatePropertyText();
    }

    public void PutOn(Item item)
    {
        Item exitItem = null;
        foreach (Slot slot in SlotList)
        {
            EquipmentSlot equipmentSlot = (EquipmentSlot)slot;
            if (equipmentSlot.IsRightItem(item))
            {
                if (equipmentSlot.transform.childCount > 0)
                {
                    ItemUI currentItemUI = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                    exitItem = currentItemUI.M_Item;
                    currentItemUI.SetItem(item, 1);
                }
                else
                {
                    equipmentSlot.StoreItem(item);
                }
                break;
            }
        }
        if (exitItem != null)
            Knapsack.M_Instance.StoreItem(exitItem);
        UpdatePropertyText();
    }

    public void PutOff(Item item)
    {
        Knapsack.M_Instance.StoreItem(item);
        UpdatePropertyText();
    }
    private void UpdatePropertyText()
    {
        int strength = 0, intellect = 0,agility=0,stamina=0,damage = 0;
        foreach (EquipmentSlot slot in SlotList)
        {
            if (slot.transform.childCount > 0)
            {
                Item item = slot.transform.GetChild(0).GetComponent<ItemUI>().M_Item;
                if(item is Equipment)
                {
                    Equipment e = (Equipment)item;
                    strength += e.M_Strength;
                    intellect += e.M_Intellect;
                    agility += e.M_Agility;
                    stamina += e.M_Stamina;
                }
                else if(item is Weapon)
                {
                    damage += ((Weapon)item).M_Damage;
                }
            }
        }
        strength += M_Player.BasicStrength;
        intellect += M_Player.BasicIntellect;
        agility += M_Player.BasicAgility;
        stamina += M_Player.BasicStamina;
        damage += M_Player.BasicDamage;
        string text = string.Format("力量：{0}\n智力：{1}\n敏捷：{2}\n体力：{3}\n攻击力：{4} ", strength, intellect, agility, stamina, damage);
        M_PropertyText.text = text;
    }
}
