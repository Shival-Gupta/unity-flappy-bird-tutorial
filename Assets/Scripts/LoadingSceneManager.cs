using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public float timer = 4f;

    private void Start()
    {
        // Start loading the next scene asynchronously with a delay
        StartCoroutine(LoadNextLevelAsync());
    }

    private IEnumerator LoadNextLevelAsync()
    {
        // Retrieve the next level's name from PlayerPrefs
        string nextSceneName = PlayerPrefs.GetString("NextLevel", null);

        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("Next Level name is invalid or not set!");
            yield break;
        }

        Debug.Log($"Loading Next Level: {nextSceneName}");

        // Wait for 4 seconds before loading the next scene (to show the loading screen)
        yield return new WaitForSeconds(timer);

        // Load the next level asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);

        // Wait until the async load is complete
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Clear the next level name from PlayerPrefs after loading
        PlayerPrefs.DeleteKey("NextLevel");
    }
}
