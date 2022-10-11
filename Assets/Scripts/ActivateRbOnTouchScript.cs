using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRbOnTouchScript : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
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
