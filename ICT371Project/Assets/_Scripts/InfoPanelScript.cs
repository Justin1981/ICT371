using UnityEngine;
using System.Collections;

using UnityEngine.UI;

/// <summary>
/// Script to enable/disable the info panel when the user
/// clicks on the model on the screen.
/// NOTE: GameObject used here to enable/disable canvas which is 
/// better then just acting on the canvas. THIS METHOD SHOULD BE USED
/// </summary>
public class InfoPanelScript : MonoBehaviour {

    //public Canvas infoCanvas;
    public GameObject infoCanvas;
    public Button okBtn;

    private bool startPanelDisabled;

	// Use this for initialization
	void Start () 
    {
        okBtn = okBtn.GetComponent<Button>();

        startPanelDisabled = false;
	}
	
	// Turn info panel off
    public void InfoPanelOff()
    {
        infoCanvas.SetActive(false);
    }

    // set panel off
    public void StartPanelDisabled()
    {
        startPanelDisabled = true;
    }

    // on mouse down set canvas active
    void OnMouseDown()
    {
        if (!infoCanvas.activeSelf && startPanelDisabled)
        {
            infoCanvas.SetActive(true);
        }
    }
}
