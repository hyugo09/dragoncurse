using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class inventoryitem 
{
   public itemdata itemdata;
    public int stacksize;

    public inventoryitem(itemdata item)
    {
        itemdata = item;
        AddtoStack();
    }

    public void AddtoStack()
    {
        stacksize++;
    }

    public void RemovefromStack()
    {
        stacksize--;
    }
}
