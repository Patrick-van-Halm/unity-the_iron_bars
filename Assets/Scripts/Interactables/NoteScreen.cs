using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteScreen : MonoBehaviour
{
    public GameObject noteDisplay;
    public TMP_Text textElement;
    public PlayerController player;

    public void SetText(string text)
    {
        textElement.text = text;
    }

    public void Active(bool show)
    {
        player.SetCanMove(!show);
        noteDisplay.SetActive(show);
    }
}
