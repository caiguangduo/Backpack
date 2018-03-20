using UnityEngine;
using System.Collections;
using System.Text;

public class Consumable:Item
{
    private int m_hp;
    private int m_mp;

    public int M_HP
    {
        get { return m_hp; }
        set { m_hp = value; }
    }
    public int M_MP
    {
        get { return m_mp; }
        set { m_mp = value; }
    }

    public Consumable(int id,string name,ItemType type,ItemQuality quality,string des,int capacity,int buyPrice,int sellPrice,string sprite,int hp,int mp)
        : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprite)
    {
        this.M_HP = hp;
        this.M_MP = mp;
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
        sb.Append(M_HP);
        sb.Append("  ");
        sb.Append(M_MP);

        return sb.ToString();
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();
        string newText = string.Format("{0}\n\n<color=blue>加血:{1}\n加蓝:{2}</color>", text, M_HP, M_MP);
        return newText;
    }
}
