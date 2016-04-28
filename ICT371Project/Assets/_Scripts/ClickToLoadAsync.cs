using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Script to load a new scene asynchronously with a loding screen
/// Loading progress updates the loading bar status on the loading screen
/// </summary>
public class ClickToLoadAsync : MonoBehaviour {

    public Slider loadingBar;
    public GameObject loadingImage;

    private AsyncOperation async;

    public void ClickAsync(int level)
    {
        loadingImage.SetActive(true);

        StartCoroutine(LoadLevelWithBar(level));
    }

    IEnumerator LoadLevelWithBar (int level)
    {
        //async = Application.LoadLevelAsync(level);
        async = SceneManager.LoadSceneAsync(level);

        while(!async.isDone)
        {
            loadingBar.value = async.progress;
            yield return null;
        }

    }

}
