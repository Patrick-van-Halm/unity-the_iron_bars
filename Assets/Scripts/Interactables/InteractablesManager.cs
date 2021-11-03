using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractablesManager : MonoBehaviour
{
    public static InteractablesManager Instance { get; private set; }
    
    public GameObject display;
    public Texture2D interactableDefaultIcon;
    public PlayerInputs input;

    private TMP_Text textElement;
    private RawImage iconElement;

    private void Awake()
    {
        if (Instance && Instance != this) Destroy(Instance);
        Instance = this;
        DontDestroyOnLoad(Instance);

        textElement = display.GetComponentInChildren<TMP_Text>();
        iconElement = display.GetComponentInChildren<RawImage>();
        input = new PlayerInputs();
    }

    public void SetIconAndText(Texture2D icon, string text)
    {
        iconElement.texture = icon;
        textElement.text = text;
    }

    public void ChangeToDefaultIconAndText()
    {
        textElement.text = "Press [" + input.CharacterControls.Interact.controls[0].displayName.ToUpper() + "] to interact.";
        iconElement.texture = interactableDefaultIcon;
    }

    public void Show(bool a)
    {
        ChangeToDefaultIconAndText();
        display.SetActive(a);
    }
}
