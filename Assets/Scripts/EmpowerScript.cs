using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpowerScript : MonoBehaviour
{
    public float EmpowerTime;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && EmpowerTime > 0)
        {
            other.GetComponent<PlayerScript>().Empower(EmpowerTime);
        }
    }
}
