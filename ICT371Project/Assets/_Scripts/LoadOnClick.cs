using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

/// <summary>
/// Script to load a new scene and display loading image
/// </summary>
public class LoadOnClick : MonoBehaviour {

    public GameObject loadingImage;

    public void LoadScene(int level)
    {
        loadingImage.SetActive(true);
        
        SceneManager.LoadScene(level);
    }
}
