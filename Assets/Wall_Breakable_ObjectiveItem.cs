using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Breakable_ObjectiveItem : Interactable
{
    public Texture2D failIcon;
    public string itemId;

    protected override void Interact()
    {
        var hasObjectiveItem = InventoryManager.Instance.Item is Item_Objective && (InventoryManager.Instance.Item as Item_Objective).id == itemId;

        if (!hasObjectiveItem)
        {
            SetIconAndText(failIcon, "This wall is too thick.");
            ResetIconAndTextAfterSeconds(1);
            return;
        }
        else
        {
            InventoryManager.Instance.DestroyItem();
            gameObject.SetActive(false);
        }
    }
}
