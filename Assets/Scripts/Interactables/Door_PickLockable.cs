using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_PickLockable : Door
{
    public Texture2D lockedIcon;
    public Texture2D unlockedIcon;

    private bool isLocked = true;
    private bool hasPicklock = false;

    protected override void Interact()
    {
        hasPicklock = InventoryManager.Instance.Item is Item_Picklock;

        if (isLocked && !hasPicklock)
        {
            SetIconAndText(lockedIcon, "Door is locked.");
            return;
        }
        else if (hasPicklock)
        {
            InventoryManager.Instance.DestroyItem();
            ChangeLockState(false);
            SetIconAndText(unlockedIcon, "Door has been unlocked.");
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
