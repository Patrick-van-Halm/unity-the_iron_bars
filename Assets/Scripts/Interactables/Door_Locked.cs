using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door_Locked : Door
{
    public Texture2D lockedIcon;
    public UnityEvent<bool> onLockstateChange;

    private bool isLocked = true;

    protected override void Interact()
    {
        if (isLocked)
        {
            SetIconAndText(lockedIcon, "Door is locked.");
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
        onLockstateChange?.Invoke(isLocked);
    }
}
