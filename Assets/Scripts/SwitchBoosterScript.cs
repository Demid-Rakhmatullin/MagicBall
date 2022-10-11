using UnityEngine;
using UnityEngine.VFX;

public class SwitchBoosterScript : MonoBehaviour
{
    public GameObject Booster;
    public GameObject StartHints;
    public GameObject FinalHints;

    private VisualEffect VFX;
    private VisualEffect VFXActive;

    void Start()
    {
        VFX = transform.Find("VFX").GetComponent<VisualEffect>();
        VFXActive = transform.Find("VFXActive").GetComponent<VisualEffect>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Booster.activeInHierarchy)
        {
            Booster.SetActive(true);
            FinalHints.SetActive(true);
            StartHints.SetActive(false);

            VFXActive.GetComponent<Renderer>().renderingLayerMask = 1;
            VFX.GetComponent<Renderer>().renderingLayerMask = 0;
        }
    }
}
