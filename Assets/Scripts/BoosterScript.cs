using UnityEngine;

public class BoosterScript : MonoBehaviour
{
    public float force;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var rigidBody = other.attachedRigidbody;
            rigidBody.velocity = Vector3.zero;
            rigidBody.AddForce(Vector3.up * force);
        }
    }
}
