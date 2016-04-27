using UnityEngine;
using System.Collections;

using System.Collections.Generic;

/// <summary>
/// Script to handle the user touching the object/model on the screen
/// and send hit data to the object so it knows it has been clicked on
/// and can perform required actions
/// </summary>
public class OnTouchDown : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit = new RaycastHit();

        for (int i = 0; i < Input.touchCount; ++i)
        {
            //Debug.Log("Touch Input Logged");
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("** HIT **");
                // Don't need this because the hit is logged via physics with OnMouseDown
                // This can be used but with a different function name but in the target game object
                //hit.transform.gameObject.SendMessage("OnMouseDown");
            }
        }
    }
}
