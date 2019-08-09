using UnityEngine;

public class ImpactScript : MonoBehaviour {
    public ParticleSystem[] particles;

    private void Update() {
        foreach (ParticleSystem particle in particles)
            if (particle.IsAlive())
                return;
        
        Destroy(gameObject);
    }
}
