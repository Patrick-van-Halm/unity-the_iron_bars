using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Locked : Door
{
    public Texture2D lockedIcon;

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
            SetDoor(State != States.Open);
        }
    }

    public void ChangeLockState(bool isLocked)
    {
        this.isLocked = isLocked;
    }
}
