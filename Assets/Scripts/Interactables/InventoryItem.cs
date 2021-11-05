using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InventoryItem : Interactable
{
    public Texture2D Image;
    public AudioClip groundHitSound;

    private Rigidbody rb;
    private AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
        if (rb.isKinematic) rb.isKinematic = false;
        gameObject.SetActive(false);
    }

    protected override void Interact()
    {
        if (InventoryManager.Instance.Item) 
        {
            //SetIconAndText(InventoryManager.Instance.inventoryFullIcon, "Inventory is full");
            //ResetIconAndTextAfterSeconds(1);
            //return;
            InventoryManager.Instance.DropItem();
        }

        Pickup();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (audioSource && groundHitSound) audioSource.PlayOneShot(groundHitSound);
    }
}
