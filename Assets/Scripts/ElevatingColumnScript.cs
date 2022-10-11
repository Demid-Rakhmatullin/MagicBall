using UnityEngine;

public class ElevatingColumnScript : MonoBehaviour
{

    public float Acceleration = 1.5f;
    public float MaxSpeed;
    public float StopElevationShift = 1;

    private new Collider collider;

    void Awake()
    {
        collider = GetComponent<Collider>();
    }

    void OnTriggerStay(Collider other)
    {
        
        var otherRb = other.attachedRigidbody;
        if (otherRb && otherRb.velocity.y < MaxSpeed)
        {
            var force = -Physics.gravity.y;
            if (collider.bounds.max.y > other.bounds.min.y + StopElevationShift)
                force *= Acceleration;
            otherRb.AddForce(Vector3.up * force, ForceMode.Acceleration);
        }
    }  
}
