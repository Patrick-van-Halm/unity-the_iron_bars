using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Flashlight : InventoryItem
{
    public GameObject lightEmitter;

    protected override void Start()
    {
        base.Start();
        FindObjectOfType<PlayerController>().flashlight.AddListener(SetEnabled);
    }

    public void SetEnabled(bool enabled)
    {
        lightEmitter.SetActive(enabled);
    }
}
