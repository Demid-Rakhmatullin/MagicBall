using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepelScript : MonoBehaviour
{
    public float Force;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerScript>().Empowered)
            {
                other.attachedRigidbody.velocity = new Vector3(0, other.attachedRigidbody.velocity.y, 0);
                var playerPos = other.transform.position;
                var repelDirection = new Vector3(playerPos.x - transform.position.x, 0, playerPos.z - transform.position.z).normalized;
                other.attachedRigidbody.AddForce(repelDirection * Force);
            }
        }
    }
}
