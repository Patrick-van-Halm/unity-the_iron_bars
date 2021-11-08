using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Locked_ObjectiveItem : Door
{
    public Texture2D lockedIcon;
    public Texture2D unlockedIcon;
    public string itemId;

    private bool isLocked = true;

    protected override void Interact()
    {
        var hasObjectiveItem = InventoryManager.Instance.Item is Item_Objective && (InventoryManager.Instance.Item as Item_Objective).id == itemId;

        if (isLocked && !hasObjectiveItem)
        {
            SetIconAndText(lockedIcon, "Door is locked.");
            return;
        }
        else if (isLocked && hasObjectiveItem)
        {
            InventoryManager.Instance.DestroyItem();
            ChangeLockState(false);
            SetIconAndText(unlockedIcon, "You have used the item to open the door.");
            ResetIconAndTextAfterSeconds(1);
            return;
        }
        else
        {
            base.Interact();
        }
    }

    public void ChangeLockState(bool isLocked)
    {
        this.isLocked = isLocked;
    }
}
