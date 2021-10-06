using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Door : Interactable
{
    public enum States
    {
        Open,
        Closed
    }

    protected States State { get; set; }

    protected virtual void SetDoor(bool open)
    {
        State = open ? States.Open : States.Closed;
    }
}
