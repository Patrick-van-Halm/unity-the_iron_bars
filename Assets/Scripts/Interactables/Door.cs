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

    public States State { get; private set; }


    public void SetDoor(bool open)
    {
        State = open ? States.Open : States.Closed;
        stateChanged?.Invoke(State); 
    }

    protected override void Interact()
    {
        SetDoor(State != States.Open);
    }

    protected override void Start()
    {
        base.Start();
        State = initialState;
    }
}
