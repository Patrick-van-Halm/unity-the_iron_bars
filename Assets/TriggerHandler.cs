using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerHandler : MonoBehaviour
{
    public UnityEvent<Collider> onTriggerEnter = new UnityEvent<Collider>();
    public UnityEvent<Collider> onTriggerExit = new UnityEvent<Collider>();
    public UnityEvent<Collider> onTriggerStay = new UnityEvent<Collider>();


    private void OnTriggerEnter(Collider other) => onTriggerEnter?.Invoke(other);
    private void OnTriggerExit(Collider other) => onTriggerExit?.Invoke(other);
    private void OnTriggerStay(Collider other) => onTriggerStay?.Invoke(other);
}
