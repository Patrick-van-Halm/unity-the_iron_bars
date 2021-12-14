using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Gear : Item_Objective
{
    public Texture2D pickupFailIcon;

    protected override void Pickup()
    {
        if (InventoryManager.Instance.Item is Item_Picklock) base.Pickup();
        else
        {
            SetIconAndText(pickupFailIcon, "I need a different way to get this gear.");
            ResetIconAndTextAfterSeconds(1);
        }
    }
}
