using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {
    
    private void Start() {
        StartCoroutine(LoadGame());
    }

    private static IEnumerator LoadGame() {
        yield return new WaitForSeconds(Random.Range(3.5f, 5f));
        SceneManager.LoadScene("GameScene_new");
    }
}
