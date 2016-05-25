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
        Visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!infoCanvas.activeSelf && Visible)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    transform.Rotate(touch.deltaPosition.x * rotationRate,
                                        -touch.deltaPosition.y * rotationRate, 0, Space.World);
                }
            }
        }
    }
}
