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

    public States initialState;
    public UnityEvent<States> stateChanged = new UnityEvent<States>();

    private States state;

    protected virtual void SetDoor(bool open)
    {
        state = open ? States.Open : States.Closed;
        stateChanged?.Invoke(state); 
    }

    protected override void Interact()
    {
        SetDoor(state != States.Open);
    }

    protected override void Start()
    {
        base.Start();
        state = initialState;
    }
}
