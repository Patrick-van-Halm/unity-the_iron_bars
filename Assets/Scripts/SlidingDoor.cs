using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public GameObject gameObject;
    public float openingSpeed = 5.0f;
    
    public Vector3 endPosition;
    private Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlideDoor(Door.States state)
    {
        if (state == Door.States.Open)
        {
            gameObject.transform.position = startPosition + endPosition;
        } 
        else
        {
            gameObject.transform.position = startPosition;
        }
    }

}
