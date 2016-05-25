using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Script to load a new scene asynchronously with a loading screen
/// Loading progress updates the loading bar status on the loading screen
/// </summary>
public class ClickToLoadAsync : MonoBehaviour {

    public Slider loadingBar; // loading progress
    public GameObject loadingImage; // loading image

    private AsyncOperation async;

    public void ClickAsync(int level)
    {
        loadingImage.SetActive(true);

        StartCoroutine(LoadLevelWithBar(level));
    }

    IEnumerator LoadLevelWithBar (int level)
    {
        async = SceneManager.LoadSceneAsync(level);

        while(!async.isDone)
        {
            loadingBar.value = async.progress;
            yield return null;
        }

    }

}
