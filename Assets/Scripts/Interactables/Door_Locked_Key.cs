using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Locked_Key : Door
{
    public Texture2D lockedIcon;
    public Texture2D unlockedIcon;

    private bool isLocked = true;
    private bool hasKey = false;

    protected override void Interact()
    {
        hasKey = InventoryManager.Instance.Item is Item_Key && (InventoryManager.Instance.Item as Item_Key).opens == this;

        if (isLocked && !hasKey)
        {
            SetIconAndText(lockedIcon, "Door is locked.");
            return;
        }
        else if (hasKey)
        {
            InventoryManager.Instance.DestroyItem();
            ChangeLockState(false);
            SetIconAndText(unlockedIcon, "Door has been unlocked.");
            return;
        }
        else
        {
            SetDoor(State != States.Open);
        }
    }

    public void ChangeLockState(bool isLocked)
    {
        this.isLocked = isLocked;
    }
}
