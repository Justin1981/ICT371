using UnityEngine;
using System.Collections;

using UnityEngine.UI;

/// <summary>
/// Script to rotate the object/model displayed on the screen
/// from the image target
/// </summary>
public class RotateTouchScript : MonoBehaviour
{
    public GameObject infoCanvas;
    public bool Visible;

    private float rotationRate = 0.5f;
    
    // Use this for initialization
    void Start()
    {
        //infoCanvas = infoCanvas.GetComponent<Canvas>();
        Visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!infoCanvas.activeSelf && Visible)
        {
            foreach (Touch touch in Input.touches)
            {
                //Debug.Log("Touching at: " + touch.position);

                //if (touch.phase == TouchPhase.Began)
                //{
                //    Debug.Log("Touch phase began at: " + touch.position);
                //}
                //else
                if (touch.phase == TouchPhase.Moved)
                {
                    //Debug.Log("Touch phase Moved");
                    transform.Rotate(touch.deltaPosition.x * rotationRate,
                                        -touch.deltaPosition.y * rotationRate, 0, Space.World);
                }
                //else if (touch.phase == TouchPhase.Ended)
                //{
                //    Debug.Log("Touch phase ended");
                //}
            }
        }
    }
}
