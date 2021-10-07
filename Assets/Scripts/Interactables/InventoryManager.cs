using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public GameObject display;
    public RawImage itemImage; 
    public Texture2D emptyTexture;
    public float throwSpeed;
    public Transform dropItemAnchor;
    public Texture2D inventoryFullIcon;

    private PlayerController playerController;

    public InventoryItem Item { get; private set; }

    private void Awake()
    {
        if (Instance && Instance != this) Destroy(Instance);
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void DropItem()
    {
        if (!Item) return;

        Item.Drop(dropItemAnchor.position, playerController.transform.forward * throwSpeed);
        Item = null;
        SetItemImage(emptyTexture);
    }

    public void PickupItem(InventoryItem item)
    {
        this.Item = item;
        SetItemImage(item.Image);
    }

    public void DestroyItem()
    {
        SetItemImage(emptyTexture);
        Destroy(Item.gameObject);
        Item = null;
    }

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerController.drop.AddListener(DropItem);

        SetItemImage(emptyTexture);
    }

    private void SetItemImage(Texture2D texture)
    {
        itemImage.texture = texture;
    }
}
