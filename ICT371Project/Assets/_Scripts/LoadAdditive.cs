using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

/// <summary>
/// Script to load and add a scene to the existing scene
/// </summary>
public class LoadAdditive : MonoBehaviour
{

    public void LoadAddOnClick(int level)
    {
        //Application.LoadLevelAdditive(level);
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
    }
}
