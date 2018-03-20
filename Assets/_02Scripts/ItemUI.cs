using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    private Item m_item;
    private int m_amount;

    public Item M_Item
    {
        get { return m_item; }
        private set { m_item = value; }
    }
    public int M_Amount
    {
        get { return m_amount; }
        private set { m_amount = value; }
    }

    private Vector3 animationScale = new Vector3(1.4f, 1.4f, 1.4f);
    private float targetScale = 1.0f;
    private float smoothing = 4;

    private Image m_itemImage;
    private Image M_ItemImage
    {
        get
        {
            if (m_itemImage == null)
            {
                m_itemImage = GetComponent<Image>();
            }
            return m_itemImage;
        }
    }
    private Text m_amountText;
    private Text M_AmountText
    {
        get
        {
            if (m_amountText == null)
            {
                m_amountText = GetComponentInChildren<Text>();
            }
            return m_amountText;
        }
    }

    private void Update()
    {
        if (transform.localScale.x != targetScale)
        {
            float scale = Mathf.Lerp(transform.localScale.x, targetScale, smoothing * Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
            if (Mathf.Abs(transform.localScale.x - targetScale) < 0.02f)
            {
                transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            }
        }
    }

    public void SetItem(Item item,int amount = 1)
    {
        transform.localScale = animationScale;
        this.M_Item = item;
        this.M_Amount = amount;
        M_ItemImage.sprite = Resources.Load<Sprite>(item.M_Sprite);
        if (this.M_Item.M_Capacity > 1)
            M_AmountText.text = M_Amount.ToString();
        else
            M_AmountText.text = "";
    }

    public void AddAmount(int amount = 1)
    {
        transform.localScale = animationScale;
        this.M_Amount += amount;
        if (M_Item.M_Capacity > 1)
            M_AmountText.text = M_Amount.ToString();
        else
            M_AmountText.text = "";
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void SetAmount(int amount)
    {
        transform.localScale = animationScale;

        this.M_Amount = amount;
        if (M_Item.M_Capacity > 1)
            M_AmountText.text = M_Amount.ToString();
        else
            M_AmountText.text = "";
    }

    public void ReduceAmount(int amount = 1)
    {
        transform.localScale = animationScale;

        this.M_Amount -= amount;
        if (M_Item.M_Capacity > 1)
            M_AmountText.text = M_Amount.ToString();
        else
            M_AmountText.text = "";
    }

    public void Exchange(ItemUI itemUI)
    {
        Item itemTemp = itemUI.M_Item;
        int amountTemp = itemUI.M_Amount;
        itemUI.SetItem(this.M_Item, this.M_Amount);
        this.SetItem(itemTemp, amountTemp);
    }
}
