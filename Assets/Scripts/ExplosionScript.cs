using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public bool WithLight;

    private Light[] lights;
    private ParticleSystem[] animations;

    void Awake()
    {
        lights = GetComponentsInChildren<Light>(true);       
        animations = GetComponentsInChildren<ParticleSystem>();
    }

    public void Explode()
    {
        if (WithLight)
        {
            foreach (var light in lights)
                light.gameObject.SetActive(true);
            StartCoroutine(ProcessLight());
        }

        foreach (var animation in animations)
            animation.Play();
    }

    private IEnumerator ProcessLight()
    {
        yield return new WaitForSeconds(2);
        foreach (var light in lights)
            light.gameObject.SetActive(false);
    }
}
