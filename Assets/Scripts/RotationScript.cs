using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float Speed = 10;
    public bool StartManually;

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
