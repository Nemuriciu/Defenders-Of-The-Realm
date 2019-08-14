using System.Collections;
using UnityEngine;

public class DefeatScript : MonoBehaviour {
    public GameObject[] flames;
    
    private StatsMenu _statsMenu;
    private AudioSource _flameAudio;
    private AudioSource _bckAudio;

    private void Start() {
        _statsMenu = GameObject.Find("Canvas").GetComponent<StatsMenu>();
        _bckAudio = GameObject.Find("EventSystem").GetComponent<AudioSource>();
        _flameAudio = GetComponent<AudioSource>();
        StartCoroutine(DestroyArtefact());
    }

    private IEnumerator DestroyArtefact() {
        yield return new WaitForSeconds(2);
        
        foreach (GameObject flame in flames)
            flame.SetActive(true);
        
        _flameAudio.Play();
        
        yield return new WaitForSeconds(0.4f);
        
        _bckAudio.Play();
        
        yield return new WaitForSeconds(4);
        
        _statsMenu.ActivatePanel(false);
    }
}
