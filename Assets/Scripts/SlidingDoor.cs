using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Animator animator;
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlideDoor(Door.States state)
    {
        if (state == Door.States.Open)
        {

            animator.SetBool("Open", true);
        } 
        else
        {

            animator.SetBool("Open", false);
        }
    }

}
