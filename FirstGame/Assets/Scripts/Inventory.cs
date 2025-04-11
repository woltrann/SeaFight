using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private Dictionary<string, int> items = new Dictionary<string, int>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(string itemName, int amount)
    {
        if (items.ContainsKey(itemName))
            items[itemName] += amount;
        else
            items[itemName] = amount;

        Debug.Log(itemName + " x" + amount + " envantere eklendi! (Toplam: " + items[itemName] + ")");
    }
}
