using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LoadingScene : MonoBehaviour {
    
    private void Start() {
        StartCoroutine(LoadGame());
    }

    private static IEnumerator LoadGame() {
        yield return new WaitForSeconds(Random.Range(4.5f, 6.5f));
        SceneManager.LoadScene("GameScene");
    }
}
