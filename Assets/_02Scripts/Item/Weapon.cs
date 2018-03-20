using UnityEngine;
using System.Collections;
using System.Text;

public class Weapon : Item
{
    public enum WeaponType
    {
        None,OffHand,MainHand
    }

    private int m_damage;
    private WeaponType m_weaponType;

    public int M_Damage
    {
        get { return m_damage; }
        set { m_damage = value; }
    }
    public WeaponType M_WeaponType
    {
        get { return m_weaponType; }
        set { m_weaponType = value; }
    }

    public Weapon(int id,string name,ItemType type,ItemQuality quality,string des,int capacity,int buyPrice,int sellPrice,string sprite,int damage,WeaponType wpType)
        : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprite)
    {
        this.M_Damage = damage;
        this.M_WeaponType = wpType;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(M_ID);
        sb.Append("  ");
        sb.Append(M_ItemType);
        sb.Append("  ");
        sb.Append(M_ItemQuality);
        sb.Append("  ");
        sb.Append(M_Description);
        sb.Append("  ");
        sb.Append(M_Capacity);
        sb.Append("  ");
        sb.Append(M_BuyPrice);
        sb.Append("  ");
        sb.Append(M_SellPrice);
        sb.Append("  ");
        sb.Append(M_Sprite);
        sb.Append("  ");
        sb.Append(M_Damage);
        sb.Append("  ");
        sb.Append(M_WeaponType);
        
        return sb.ToString();
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();

        string wpTypeText = "";

        switch (M_WeaponType)
        {
            case WeaponType.OffHand:
                wpTypeText = "副手";
                break;
            case WeaponType.MainHand:
                wpTypeText = "主手";
                break;
        }

        string newText = string.Format("{0}\n\n<color=blue>武器类型：{1}\n攻击力：{2}</color>", text, wpTypeText, M_Damage);

        return newText;
    }
}
