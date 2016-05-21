using UnityEngine;
using System.Collections;

public class InfoPanelOn : MonoBehaviour
{
    // Called when object is touched on screen
    void OnMouseDown()
    {
        SceneController controller = GameObject.FindObjectOfType<SceneController>();

        controller.InfoPanelOn();
    }
}
