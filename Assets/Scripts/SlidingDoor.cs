using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator.SetBool("Open", false);
    }

    public void SlideDoor(Door.States state)
    {
        animator.SetBool("Open", state == Door.States.Open);
    }
}
