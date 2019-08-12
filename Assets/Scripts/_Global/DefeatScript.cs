using System.Collections;
using UnityEngine;

public class DefeatScript : MonoBehaviour {
    public GameObject[] flames;
    
    private StatsMenu _statsMenu;

    private void Start() {
        _statsMenu = GameObject.Find("Canvas").GetComponent<StatsMenu>();
        StartCoroutine(DestroyArtefact());
    }

    private IEnumerator DestroyArtefact() {
        yield return new WaitForSeconds(2);
        
        foreach (GameObject flame in flames)
            flame.SetActive(true);
        
        yield return new WaitForSeconds(3);
        
        _statsMenu.ActivatePanel();
    }
}
