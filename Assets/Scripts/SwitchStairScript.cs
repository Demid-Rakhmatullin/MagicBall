using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SwitchStairScript : MonoBehaviour
{
    public GameObject Stair;
    public float ActivenSeconds = 2f;

    private VisualEffect VFX;
    private VisualEffect VFXActive;
    private bool isActive;

    void Start()
    {
        VFX = transform.Find("VFX").GetComponent<VisualEffect>();
        VFXActive = transform.Find("VFXActive").GetComponent<VisualEffect>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            Stair.SetActive(true);
            foreach (Transform child in Stair.transform)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<DisappearScript>().SwitchActivated();
            }

            VFXActive.GetComponent<Renderer>().renderingLayerMask = 1;
            VFX.GetComponent<Renderer>().renderingLayerMask = 0;
            StartCoroutine(AfterExplosion());
            isActive = true;
        }
    }

    private IEnumerator AfterExplosion()
    {
        yield return new WaitForSeconds(ActivenSeconds);

        isActive = false;
        VFX.GetComponent<Renderer>().renderingLayerMask = 1;
        VFXActive.GetComponent<Renderer>().renderingLayerMask = 0;
    }
}
