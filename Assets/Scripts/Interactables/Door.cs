using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Door : Interactable
{
    public enum States
    {
        Open,
        Closed
    }

    public UnityEvent<States> stateChanged = new UnityEvent<States>();

    protected States State { get; set; }

    protected virtual void SetDoor(bool open)
    {
        State = open ? States.Open : States.Closed;
        stateChanged?.Invoke(State); 
    }
}
