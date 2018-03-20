using UnityEngine;
using System.Collections;

public class Chest : Inventory
{
    private static Chest m_instance;
    public static Chest M_Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.Find("ChestPanel").GetComponent<Chest>();
            }
            return m_instance;
        }
    }
}
