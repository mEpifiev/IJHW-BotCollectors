using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    public event Action<Resource> Released;


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Reset()
    {
        Rigidbody.isKinematic = false;
    }

    public void DisablePhysics()
    {
        Rigidbody.isKinematic = true;
    }

    public void Release()
    {
        Reset();
        Released?.Invoke(this);
    }
}
