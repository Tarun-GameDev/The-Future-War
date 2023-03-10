using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject levelLoaderScreen;

    public void LoadLevel(int LevelNo)
    {
        StartCoroutine(LoadSceneEnumrator(LevelNo));
    }

    IEnumerator LoadSceneEnumrator(int LevelNo)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(LevelNo);

        while(!operation.isDone)
        {
            levelLoaderScreen.SetActive(true);
            yield return null;
        }
    }

}
