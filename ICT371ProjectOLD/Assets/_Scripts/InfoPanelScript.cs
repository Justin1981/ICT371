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
        //infoCanvas = infoCanvas.GetComponent<Canvas>();
        //infoCanvas.enabled = false;

        okBtn = okBtn.GetComponent<Button>();

        startPanelDisabled = false;
	}
	
	// Turn info panel off
    public void InfoPanelOff()
    {
        //infoCanvas.enabled = false;
        infoCanvas.SetActive(false);
    }

    public void StartPanelDisabled()
    {
        startPanelDisabled = true;
    }

    void OnMouseDown()
    {
        //Debug.Log("OnMouseDown Called");
        //if (!infoCanvas.enabled)
        //{
        //    infoCanvas.enabled = true;
        //}
        if (!infoCanvas.activeSelf && startPanelDisabled)
        {
            infoCanvas.SetActive(true);
        }
    }
}
