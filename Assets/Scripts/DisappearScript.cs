using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearScript : MonoBehaviour
{
    public float DisappearSeconds;
    public Material ActiveMatereial;

    private bool isTriggered;
    private bool switchActivated;
    private new Renderer renderer;
    private Material material;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        material = renderer.material;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            if (!isTriggered)
            {
                isTriggered = true;
                switchActivated = false;
                renderer.material = ActiveMatereial;
                Invoke("Deactivate", DisappearSeconds);
            }
        }
    }

    private void Deactivate()
    {
        renderer.material = material;
        if (!switchActivated)
            gameObject.SetActive(false);
        isTriggered = false;
    }

    public void SwitchActivated()
    {
        switchActivated = true;
    }
}
