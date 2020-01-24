using UnityEngine;

public class HitEffect : MonoBehaviour {
    public ParticleSystem[] particles;
    
    private void Update() {
        /* Destroy object when all particles finished */
        foreach (var p in particles)
            if (p.IsAlive())
                return;
        
        Destroy(gameObject);
    }
}
