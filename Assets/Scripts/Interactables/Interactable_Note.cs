using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Note : Interactable
{
    [TextArea(15, 30)]
    public string text;

    protected override void Interact()
    {
        InteractablesManager.Instance.noteScreen.SetText(text);
        InteractablesManager.Instance.noteScreen.Active(true);
    }
}
