using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    public List<inventoryitem> Inventory = new List<inventoryitem>();
    private Dictionary<itemdata, inventoryitem> itemdictionary = new Dictionary<itemdata, inventoryitem>();
    public static inventory inventaire;
 

    private void OnEnable()
    {
        testcollectible.Onthiscollected += Add;
    }
    private void OnDisable()
    {
        testcollectible.Onthiscollected -= Add;
    }
    public void Add(itemdata itemdata)
    {// ajoute au stact si ya deja 1 objet avec meme itemdata
        if (itemdictionary.TryGetValue(itemdata, out inventoryitem item))
        {
            item.AddtoStack();
            //Debug.Log($"{item.itemdata.displayName} total stack is now {item.stacksize}");
        }
        else
        {
            // cree un nouveau stack
            inventoryitem newitem = new inventoryitem(itemdata);
            Inventory.Add(newitem);
            itemdictionary.Add(itemdata, newitem);
            Debug.Log($"{newitem.itemdata.displayName} est collecter pour la premiere fois");
        }
    }

    public void Remove(itemdata itemdata)
    {
        if (itemdictionary.TryGetValue(itemdata, out inventoryitem item))
        {
            item.RemovefromStack();
            if(item.stacksize == 0)
            {
                Inventory.Remove(item);
                itemdictionary.Remove(itemdata);
            }

        }
    }
}
