using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class DestroyWallScript : MonoBehaviour
{
    public GameObject Wall;
    public GameObject RuinedWall;
    public ExplosionScript Explosion;
    public float ExplosionSeconds;

    private VisualEffect VFXActive;

    void Awake()
    {
        VFXActive = transform.Find("VFXActive").GetComponent<VisualEffect>();    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Wall.activeInHierarchy)
            {
                Explosion.Explode();
                VFXActive.GetComponent<Renderer>().renderingLayerMask = 1;
                StartCoroutine(AfterExplosion(ExplosionSeconds));
            }          
        }
    }

    private IEnumerator AfterExplosion(float explosionSeconds)
    {
        yield return new WaitForSeconds(explosionSeconds);

        Wall.SetActive(false);
        RuinedWall.SetActive(true);
        VFXActive.GetComponent<Renderer>().renderingLayerMask = 0;
    }
}
