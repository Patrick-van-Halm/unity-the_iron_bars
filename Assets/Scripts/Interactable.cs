using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour, IRaycastable3D
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        
    }
    protected abstract void Interact();
    public void SetIconAndText(Texture2D icon, string text) => InteractablesManager.Instance.SetIconAndText(icon, text);
    public void OnRaycastEnter(RaycastHit hit)
    {
        InteractablesManager.Instance.Show(true);
        playerController.interact.AddListener(Interact);
    }
    public void OnRaycastStay(RaycastHit hit){}
    public void OnRaycastExit()
    {
        InteractablesManager.Instance.Show(false);
        playerController.interact.RemoveListener(Interact);
    }
}
