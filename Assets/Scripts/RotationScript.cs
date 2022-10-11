using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float Speed = 10;
    public bool StartManually;

    // Start is called before the first frame update
    void Start()
    {
        if (!StartManually)
        {
            _startRotation();
        }
    }

    public void StartRotation() => _startRotation();  

    private void _startRotation()
    {
        var rb = GetComponent<Rigidbody>();
        rb.AddTorque(Vector3.up * Speed);
    }

    public void StopRotation()
    {
        var rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Vector3.zero;
    }
}
