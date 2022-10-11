using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    public GameObject target;

    private bool toggled;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            target.GetComponent<MeshRenderer>().enabled = !toggled;
            RotationScript rotation = target.GetComponent<RotationScript>();
            if (rotation != null)
                if (!toggled) rotation.StartRotation();
                else rotation.StopRotation();
            toggled = !toggled;
        }
    }
}
