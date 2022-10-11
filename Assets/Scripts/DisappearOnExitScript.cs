using UnityEngine;

public class DisappearOnExitScript : MonoBehaviour
{
    void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {            
            Destroy(gameObject);  
        }
    }
}
