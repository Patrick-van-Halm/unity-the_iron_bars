using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Flashlight : InventoryItem
{
    public GameObject lightEmitter;

    private PlayerController playerController;
    protected override void Start()
    {
        base.Start();
        playerController = FindObjectOfType<PlayerController>();
    }

    public override void Drop(Vector3 pos, Vector3 velocity)
    {
        base.Drop(pos, velocity);
        SetEnabled(false);
        playerController.secondaryInteract.RemoveListener(SetEnabled);
    }

    protected override void Pickup()
    {
        base.Pickup();
        playerController.secondaryInteract.AddListener(SetEnabled);
    }

    public void SetEnabled(bool enabled)
    {
        lightEmitter.SetActive(enabled);
    }
}
