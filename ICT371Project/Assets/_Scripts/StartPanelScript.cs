using UnityEngine;
using System.Collections;

using UnityEngine.UI;

/// <summary>
/// Script to disable the canvas shown when the scene first loads.
/// NOTE: Canvas used here to enable/disable canvas which is 
/// not as good as acting on the GameObject. THIS METHOD SHOULD NOT BE USED
/// </summary>
public class StartPanelScript : MonoBehaviour {
    
    public Canvas startCanvas;
    public Button okBtn;

    // Use this for initialization
    void Start()
    {
        startCanvas = startCanvas.GetComponent<Canvas>();
        okBtn = okBtn.GetComponent<Button>();
    }

    // Update is called once per frame
    public void StartPanelOff()
    {
        startCanvas.enabled = false;
    }

}
