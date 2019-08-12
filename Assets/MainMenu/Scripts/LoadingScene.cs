using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LoadingScene : MonoBehaviour {
    
    private void Start() {
        StartCoroutine(LoadGame());
    }

    private static IEnumerator LoadGame() {
        yield return new WaitForSeconds(Random.Range(3.0f, 6.0f));
        SceneManager.LoadScene("GameScene");
    }
}
