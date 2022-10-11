using UnityEngine;

public class ActivateRbOnTouchScript : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && rb.isKinematic)
        {
            rb.isKinematic = false;
        }
    }
}
