using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InventoryItem : Interactable
{
    public Texture2D Image;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Drop(Vector3 pos, Vector3 velocity)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        rb.velocity = velocity;
    }

    protected virtual void Pickup()
    {
        InventoryManager.Instance.PickupItem(this);
        gameObject.SetActive(false);
    }

    protected override void Interact()
    {
        if (InventoryManager.Instance.Item) 
        {
            SetIconAndText(InventoryManager.Instance.inventoryFullIcon, "Inventory is full");
            ResetIconAndTextAfterSeconds(1);
            return;
        }

        Pickup();
    }
}
