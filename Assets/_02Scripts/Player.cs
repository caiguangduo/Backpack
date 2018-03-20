using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int basicStrength = 10;
    private int basicIntellect = 10;
    private int basicAgility = 10;
    private int basicStamina = 10;
    private int basicDamage = 10;

    public int BasicStrength
    {
        get { return basicStrength; }
    }
    public int BasicIntellect
    {
        get { return basicIntellect; }
    }
    public int BasicAgility
    {
        get { return basicAgility; }
    }
    public int BasicStamina
    {
        get { return basicStamina; }
    }
    public int BasicDamage
    {
        get { return basicDamage; }
    }

    private int coinAmount = 100;
    public int CoinAmount
    {
        get { return coinAmount; }
        set { coinAmount = value; }
    }
    private Text coinText;
    private Text CoinText
    {
        get
        {
            if (coinText == null)
            {
                coinText = GameObject.Find("Coin").GetComponentInChildren<Text>();
            }
            return coinText;
        }
    }

    private void Start()
    {
        CoinText.text = CoinAmount.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            int id = Random.Range(1, 18);
            Knapsack.M_Instance.StoreItem(id);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Knapsack.M_Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Chest.M_Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            CharacterPanel.M_Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Vendor.M_Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Forge.M_Instance.DisplaySwitch();
        }
    }

    public bool ConsumeCoin(int amount)
    {
        if (CoinAmount >= amount)
        {
            CoinAmount -= amount;
            CoinText.text = CoinAmount.ToString();
            return true;
        }
        return false;
    }

    public void EarnCoin(int amount)
    {
        this.CoinAmount += amount;
        CoinText.text = CoinAmount.ToString();
    }
}
