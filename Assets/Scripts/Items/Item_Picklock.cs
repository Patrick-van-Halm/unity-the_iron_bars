using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Picklock : InventoryItem
{
    public int maxUses;

    public int Uses { get; private set; }

    public void Use()
    {
        Uses += 1;
    }
}
