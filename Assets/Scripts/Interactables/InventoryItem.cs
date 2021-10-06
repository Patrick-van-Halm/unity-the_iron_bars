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

    public void Drop(Vector3 pos, Vector3 velocity)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        rb.velocity = velocity;
    }

    protected override void Interact()
    {
        InventoryManager.Instance.PickupItem(this);
        gameObject.SetActive(false);
    }
}
