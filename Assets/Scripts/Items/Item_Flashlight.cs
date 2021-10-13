using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Flashlight : InventoryItem
{
    public GameObject playerLightEmitter;
    public GameObject itemLightEmitter;
    protected override void Start()
    {
        base.Start();
    }

    public override void Drop(Vector3 pos, Vector3 velocity)
    {
        base.Drop(pos, velocity);
        itemLightEmitter.SetActive(playerLightEmitter.activeSelf);
        SetEnabled(false);
        playerController.secondaryInteract.RemoveListener(SetEnabled);
    }

    protected override void Pickup()
    {
        base.Pickup();
        SetEnabled(itemLightEmitter.activeSelf);
        itemLightEmitter.SetActive(false);
        playerController.secondaryInteract.AddListener(SetEnabled);
    }

    public void SetEnabled(bool enabled)
    {
        playerLightEmitter.SetActive(enabled);
    }
}
