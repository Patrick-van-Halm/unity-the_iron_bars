using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_PickLockable : Door
{
    public Texture2D lockedIcon;
    public Texture2D unlockedIcon;

    private bool isLocked = true;

    protected override void Interact()
    {
        var picklock = InventoryManager.Instance.Item as Item_Picklock;

        if (isLocked && !picklock)
        {
            SetIconAndText(lockedIcon, "Door is locked.");
            return;
        }
        else if (isLocked && picklock)
        {
            picklock.Use();
            if (picklock.Uses >= picklock.maxUses) InventoryManager.Instance.DestroyItem();
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
