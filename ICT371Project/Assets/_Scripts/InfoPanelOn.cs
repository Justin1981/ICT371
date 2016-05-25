using UnityEngine;
using System.Collections;

/// <summary>
/// Set panel status
/// </summary>
public class InfoPanelOn : MonoBehaviour
{
    // Called when object is touched on screen
    void OnMouseDown()
    {
        SceneController controller = GameObject.FindObjectOfType<SceneController>();

        controller.InfoPanelOn();
    }
}
