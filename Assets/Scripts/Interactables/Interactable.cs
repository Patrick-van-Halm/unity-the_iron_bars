using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;

public abstract class Interactable : MonoBehaviour, IRaycastable3D
{
    protected PlayerController playerController;

    private Material outlineMaterial;
    private Dictionary<MeshRenderer, Material[]> rendererMaterials = new Dictionary<MeshRenderer, Material[]>();

    protected virtual void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        outlineMaterial = new Material(Shader.Find("Outline/Regular"));
        outlineMaterial.SetColor("_Color", Color.red);
        outlineMaterial.SetFloat("_Thickness", 1);

        var renderers = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            rendererMaterials.Add(renderers[i], renderers[i].materials);
        }
    }
    protected void ResetIconAndTextAfterSeconds(float seconds) => InteractablesManager.Instance.ResetIconAndTextAfterSeconds(seconds);
    protected abstract void Interact();
    public void SetIconAndText(Texture2D icon, string text) => InteractablesManager.Instance.SetIconAndText(icon, text);
    public virtual void OnRaycastEnter(RaycastHit hit)
    {
        InteractablesManager.Instance.Show(true);
        playerController?.primaryInteract.AddListener(Interact);
        for (int i = 0; i < rendererMaterials.Count; i++)
        {
            var renderer = rendererMaterials.Keys.ElementAt(i);
            var newMats = renderer.materials.ToList();
            newMats.Add(outlineMaterial);
            renderer.materials = newMats.ToArray();
        }
    }
    public void OnRaycastStay(RaycastHit hit){}
    public virtual void OnRaycastExit()
    {
        InteractablesManager.Instance.Show(false);
        playerController?.primaryInteract.RemoveListener(Interact);
        for (int i = 0; i < rendererMaterials.Count; i++)
        {
            var pair = rendererMaterials.ElementAt(i);
            pair.Key.materials = pair.Value;
        }
    }
}
